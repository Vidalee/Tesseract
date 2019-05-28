using Script.GlobalsScript.Struct;
using UnityEngine;

public class EnemiesLive : MonoBehaviour
{
    [SerializeField] protected EnemyData Enemy;
    [SerializeField] protected GameObject weapon;
    [SerializeField] protected Weapons weaponData;
    public GameEvent SendPlayerXp;
    private bool alive = true;
    
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
        if (alive)
        {
            alive = false;
            GameObject newWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
            Weapons newWeaponData = ScriptableObject.CreateInstance<Weapons>();
            newWeaponData.Create(weaponData);
            newWeapon.GetComponent<WeaponManager>().Create(weaponData);
            Destroy(gameObject);
            SendPlayerXp.Raise(new EventArgsInt(Enemy.XpValue));
        }
    }

    public void Create(EnemyData enemy, Weapons weapon)
    {
        Enemy = enemy;
        weaponData = weapon;
    }
}