using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Serialization;

public class Attack : MonoBehaviour
{
    [SerializeField] protected Enemy Enemy;

    private float cooldown;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(Cooldown());
    }

    public void TryAttack()
    {
        if ((transform.position - player.transform.position).sqrMagnitude < Enemy.AttackRange * Enemy.AttackRange)
        {
            Live script = player.GetComponent<Live>();
            script.Damage(Enemy.PhysicsDamage);
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

    public void Create(Enemy enemy)
    {
        Enemy = enemy;
    }
}