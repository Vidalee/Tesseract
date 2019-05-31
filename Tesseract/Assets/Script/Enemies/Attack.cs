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
    public bool CanAttack;

    private void Start()
    {
        StartCoroutine(Cooldown());
        CanAttack = true;
    }

    public void TryAttack()
    {
        if (CanAttack && (transform.position - player.transform.position).sqrMagnitude < Enemy.AttackRange * Enemy.AttackRange + 0.5f)
        {
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
}