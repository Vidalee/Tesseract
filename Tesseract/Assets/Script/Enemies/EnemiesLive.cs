using UnityEngine;

public class EnemiesLive : MonoBehaviour
{
    [SerializeField] protected int live;

    public void GetDamaged(int damage)
    {
        live -= damage;
        if (live <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}