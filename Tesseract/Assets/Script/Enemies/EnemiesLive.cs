using UnityEngine;

public class EnemiesLive : MonoBehaviour
{
    [SerializeField] protected Enemy Enemy;


    public void GetDamaged(int damage)
    {
        Enemy.Hp -= damage;
        if (Enemy.Hp <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void Create(Enemy enemy)
    {
        Enemy = enemy;
    }
}