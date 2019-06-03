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
    public LayerMask blockingLayer;

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
            _a.Play("AttackL");
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
            players = players.Where(p => p != null).ToList();
            if (players.Count == 0)
            {
                return;
            }
            Transform target = players.OrderBy(p => (p.position - transform.position).magnitude).ToList()[0];
            if ((target.position - transform.position).sqrMagnitude < Enemy.AttackRange * Enemy.AttackRange + 0.1f &&
                !waitingCooldown)
            {
                if (Enemy.Name == "Archer" || Enemy.Name == "Ghost")
                {
                    RaycastHit2D linecast = Physics2D.Linecast(transform.position + new Vector3(0, 0.5f), target.position, blockingLayer);
                    if (!linecast)
                    {
                        TryAttack(target);
                        StartCoroutine(Cooldown());
                    }
                }
                else
                {
                    TryAttack(target);
                    StartCoroutine(Cooldown());
                }
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