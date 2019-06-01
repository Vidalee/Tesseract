using System.Collections;
using System.Collections.Generic;
using Script.GlobalsScript.Struct;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesLive : MonoBehaviour
{
    public Sprite[] SpriteEffect;
    [SerializeField] protected EnemyData Enemy;
    [SerializeField] protected GameObject weapon;
    [SerializeField] protected GameObject armory;
    [SerializeField] protected List<PlayerData> playerDatas;
    [SerializeField] protected GameObject hpBar; 
    

    private SpriteRenderer Icon;

    public GameEvent SendPlayerHp;
    public GameEvent SendPlayerXp;
    private bool alive = true;

    private void Start()
    {
        Icon = transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();
    }

    public void GetDamaged(int damage)
    {
        Enemy.Hp -= damage;
        hpBar.transform.localScale = new Vector3((float) Enemy.Hp/Enemy.MaxHp * 0.01581096f, hpBar.transform.localScale.y);

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
            PlayerData player = playerDatas[Random.Range(0, playerDatas.Count)];
            ArmoryManager armoryManager = armory.GetComponent<ArmoryManager>();
            Weapons weaponData = armoryManager.GetWeaponData(player.Name);
            armoryManager.CreateWeapon(weaponData, transform, player.Lvl + 1);
            //TODO Remplacer 1 par l'étage
            Destroy(gameObject);
            SendPlayerXp.Raise(new EventArgsInt(Enemy.XpValue));
        }
    }

    public void Create(EnemyData enemy, List<PlayerData> playerDatas)
    {
        Enemy = enemy;
        this.playerDatas = playerDatas;
    }

    public void Effect(int effect, int damage, float duration)
    {
        if (effect == 1)
        {
            StartCoroutine(ResetSpeed(Enemy.MoveSpeed, duration));
            Enemy.MoveSpeed -= Enemy.MoveSpeed * ((float) damage / 100);
            StartCoroutine(SetIcon(effect, duration));
        }
        if (effect == 2)
        {
            StartCoroutine(DamagePoison(damage, duration));
            StartCoroutine(SetIcon(effect, duration));
        }
        if (effect == 3)
        {
            StartCoroutine(Stun(duration));
            StartCoroutine(SetIcon(effect, duration));
        }
        if (effect == 4)
        {
            StartCoroutine(DamageFire(damage, duration));
            StartCoroutine(SetIcon(effect, duration));
        }
        
    }

    IEnumerator ResetSpeed(float reset, float duration)
    {
        yield return new WaitForSeconds(duration);
        Enemy.MoveSpeed = reset;
    }

    IEnumerator DamagePoison(int damage, float duration)
    {
        for (int i = 0; i < duration; i++)
        {
            GetDamaged(damage);
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
            GetDamaged(damage);
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