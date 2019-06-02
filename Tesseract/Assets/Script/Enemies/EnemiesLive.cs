using System.Collections;
using System.Collections.Generic;
using Script.GlobalsScript;
using Script.GlobalsScript.Struct;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesLive : MonoBehaviour
{
    public Sprite[] SpriteEffect;
    [SerializeField] protected EnemyData Enemy;
    [SerializeField] protected GameObject armory;
    [SerializeField] protected List<PlayerData> playerDatas;
    [SerializeField] protected GameObject hpBar; 

    private SpriteRenderer Icon;

    public GameEvent SendPlayerHp;
    public GameEvent SendPlayerXp;
    private bool alive = true;

    [SerializeField] protected LayerMask weaponLayer;
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected LayerMask bossLayer;
    [SerializeField] protected CircleCollider2D circleCollider2D;

    
    private void Start()
    {
        Icon = transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();
    }

    public void GetDamaged(int damageP, int damageM)
    {
        int dP = damageP - Enemy.ArmorP;
        int dM = damageM - Enemy.ArmorM;
        
        if(dP < 0) dP = 0;
        if (dM < 0) dM = 0;

        Enemy.Hp -= dP + dM;
            
        if (Enemy.Hp <= 0)
        {
            Death();
        }
        
        hpBar.transform.localScale = new Vector3((Enemy.Hp/Enemy.MaxHp) * 0.01581096f, hpBar.transform.localScale.y);
    }

    public void Death()
    {
        if (alive)
        {
            alive = false;
            if (!circleCollider2D.IsTouchingLayers(weaponLayer) && !circleCollider2D.IsTouchingLayers(enemyLayer) 
                                                                && !circleCollider2D.IsTouchingLayers(bossLayer))
            {
                if (Random.Range(0, 7) * 0 == 0)
                {
                    PlayerData player = playerDatas[Random.Range(0, playerDatas.Count)];
                    ArmoryManager armoryManager = armory.GetComponent<ArmoryManager>();
                    Weapons weaponData = armoryManager.GetWeaponData(player.Name);
                    armoryManager.CreateWeapon(weaponData, transform, Enemy.Lvl);
                }
            }

            Destroy(gameObject);
            SendPlayerXp.Raise(new EventArgsInt((int)Enemy.XpValue));
        }
    }

    public void Create(EnemyData enemy, List<PlayerData> playerDatas)
    {
        Enemy = enemy;
        this.playerDatas = playerDatas;
    }

    public void Effect(int effect, int damage, float duration)
    {
        switch (effect)
        {
            case 1:
                StartCoroutine(ResetSpeed(damage, duration));
                break;
            case 2:
                StartCoroutine(DamagePoison(damage, duration));
                break;
            case 3:
                StartCoroutine(Stun(duration));
                break;
            case 4:
                StartCoroutine(DamageFire(damage, duration));
                break;
        }

        StartCoroutine(SetIcon(effect, duration));

    }

    IEnumerator ResetSpeed(float damage, float duration)
    {
        float old = Enemy.MoveSpeed;
        Enemy.MoveSpeed -= 1 + damage / 100;
        if (Enemy.MoveSpeed < 0) Enemy.MoveSpeed = 0;
        yield return new WaitForSeconds(duration);
        Enemy.MoveSpeed = old;
    }

    IEnumerator DamagePoison(int damage, float duration)
    {
        for (int i = 0; i < duration; i++)
        {
            GetDamaged(0, damage);
            SendPlayerHp.Raise(new EventArgsInt(damage / 2));
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Stun(float duration)
    {
        float old = Enemy.MoveSpeed;
        GetComponent<Attack>().CanAttack = false;
        Enemy.MoveSpeed = 0;
        yield return new WaitForSeconds(duration);
        GetComponent<Attack>().CanAttack = false;
        Enemy.MoveSpeed = old;
    }
    
    IEnumerator DamageFire(int damage, float duration)
    {
        for (int i = 0; i < duration; i++)
        {
            GetDamaged(damage, 0);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void AddEmote(int index)
    {
        Icon.sprite = SpriteEffect[index];
    }

    public void RemoveEmote()
    {
        Icon.sprite = SpriteEffect[0];
    }

    public IEnumerator SetIcon(int index, float time)
    {
        AddEmote(index);
        yield return new WaitForSeconds(time);
        RemoveEmote();
    }
}