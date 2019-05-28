using System;
using System.Collections;
using Script.GlobalsScript.Struct;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] protected EnemyData Enemy;
    public GameEvent PlayerDamage;

    private float cooldown;
    private Transform player;

    private void Start()
    {
        StartCoroutine(Cooldown());
    }

    public void TryAttack()
    {
        if ((transform.position - player.transform.position).sqrMagnitude < Enemy.AttackRange * Enemy.AttackRange + 0.5f)
        {
            if (Enemy.MaxHp == 100) InstantiateProjectiles(Enemy.GetCompetence("AutoAttack"), (player.position - transform.position).normalized);
            PlayerDamage.Raise(new EventArgsInt(Enemy.PhysicsDamage));
        }
    }
  
    IEnumerator Cooldown()
    {
        for (;;)
        {
            TryAttack();
            yield return new WaitForSeconds(Enemy.MaxCooldown);  
        } 
    }

    public void Create(EnemyData enemy, Transform player)
    {
        Enemy = enemy;
        this.player = player;
    }
    
    private void InstantiateProjectiles(CompetencesData competence, Vector3 dir)
    {
        Transform o = Instantiate(competence.Object, transform.position + dir + new Vector3(0, 0.5f), Quaternion.identity);
        o.name = competence.Name;
                
        ProjectilesData projectilesData = ScriptableObject.CreateInstance<ProjectilesData>();
        projectilesData.Created(dir, competence.Speed, competence.Damage, competence.Tag, competence.AnimationClip, competence.Live, competence.Color);
        
        Projectiles script = o.GetComponent<Projectiles>();

        script.Create(projectilesData);
    }
}