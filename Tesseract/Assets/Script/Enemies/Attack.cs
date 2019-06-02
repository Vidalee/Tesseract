using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.GlobalsScript.Struct;
using UnityEngine;
using UnityEngine.Playables;

public class Attack : MonoBehaviour
{
    [SerializeField] protected EnemyData Enemy;
    
    public Transform Projectiles;
    public AnimationClip Archer;
    public AnimationClip Sans;
    public GameEvent PlayerDamage;
    private Animator _a;
    private SpriteRenderer _sprite;
    private bool waitingCooldown = false;

    private float cooldown;
    [SerializeField] protected List<Transform> players;
    public bool CanAttack = true;


    public void TryAttack(Transform player)
    {
        Transform target = players.OrderBy(p => (p.position - transform.position).magnitude).ToList()[0];

        Vector3 dir = (target.position - transform.position).normalized;
        
        if (Enemy.Name == "Archer")
        {
            _a.Play(player.position.x < transform.position.x ? "AttackL" : "AttackR");
            AttackProj(Archer, dir, 4.5f);
        }
        else if (Enemy.Name == "Ghost")
        {
            AttackProj(Sans, dir, 3);
            AttackProj(Sans, Quaternion.Euler(0, 0, 10) * dir, 4);
            AttackProj(Sans, Quaternion.Euler(0, 0, -10) * dir, 4);
        }
        else
        {
            _a.Play("AttackL");
        }
        
        if (Enemy.Name != "Archer" && Enemy.Name != "Ghost") PlayerDamage.Raise(new EventArgsInt(Enemy.PhysicsDamage)); 
    }

    public void AttackProj(AnimationClip clip, Vector3 dir, float speed)
    {

        Transform o = Instantiate(Projectiles, transform.position + dir/4, Quaternion.identity);
            
        ProjectilesData projectilesData = ScriptableObject.CreateInstance<ProjectilesData>();

        int dP = Enemy.PhysicsDamage;
        
        Debug.Log(dP);
    
        projectilesData.Created(dir.normalized, speed, dP, 0, "Player", clip,
            1, Color.black, 0, 0, 0, 0);
    
        Projectiles script = o.GetComponent<Projectiles>();

        script.Create(projectilesData);
        }
    
    IEnumerator Cooldown()
    {
        waitingCooldown = true;
        yield return new WaitForSeconds(Enemy.MaxCooldown);
        waitingCooldown = false;
    }

    private void Update()
    {
        _sprite.sortingOrder = (int) (transform.position.y * -10);
        if (Enemy.Triggered)
        {
            Transform target = players.OrderBy(p => (p.position - transform.position).magnitude).ToList()[0];
            if ((target.position - transform.position).sqrMagnitude < Enemy.AttackRange * Enemy.AttackRange + 0.5f &&
                !waitingCooldown)
            {
                TryAttack(target);
                StartCoroutine(Cooldown());
            }
        }
    }

    public void Create(EnemyData enemy, List<Transform> players, Animator animator)
    {
        Enemy = enemy;
        this.players = players;
        _a = animator;
        _sprite = _a.gameObject.GetComponent<SpriteRenderer>();
    }
}