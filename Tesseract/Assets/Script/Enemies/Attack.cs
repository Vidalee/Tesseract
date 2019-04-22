using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Serialization;

public class Attack : MonoBehaviour
{
    [SerializeField] protected Enemy Enemy;
    public GameEvent PlayerDamage;

    private float cooldown;
    private Transform player;

    private void Start()
    {
        StartCoroutine(Cooldown());
    }

    public void TryAttack()
    {
        if ((transform.position - player.transform.position).sqrMagnitude < Enemy.AttackRange * Enemy.AttackRange)
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

    public void Create(Enemy enemy, Transform player)
    {
        Enemy = enemy;
        this.player = player;
    }
}