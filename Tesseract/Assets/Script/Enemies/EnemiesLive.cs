﻿using System.Collections.Generic;
using Script.GlobalsScript.Struct;
using UnityEngine;

public class EnemiesLive : MonoBehaviour
{
    [SerializeField] protected EnemyData Enemy;
    [SerializeField] protected GameObject weapon;
    [SerializeField] protected GameObject armory;
    [SerializeField] protected List<PlayerData> playerDatas;
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
}