using System;
using UnityEngine;

[Serializable]
public class PlayerDataSave
{
    public int MaxHp;
    public int MaxMana;
    public int PhysicsDamage;
    public int MagicDamage;
    public float MoveSpeed;
    public int Xp;
    public int MaxXp;
    public int Lvl;
    public int ManaRegen;
    
    public int weapon;
    public int weaponLvl;

    public PlayerDataSave(PlayerData player)
    {
        MaxHp = player.MaxHp;
        MaxMana = player.MaxMana;
        PhysicsDamage = player.PhysicsDamage;
        MagicDamage = player.MagicDamage;
        MoveSpeed = player.MoveSpeed;
        Xp = player.Xp;
        MaxXp = player.MaxXp;
        Lvl = player.Lvl;
        ManaRegen = player.ManaRegen;        

        
        
        weapon = ItemNull(player.Inventory.Weapon);
        weaponLvl = player.Inventory.Weapon == null ? 0 : Lvl;
    }

    private int ItemNull(GamesItem item)
    {
        return item == null ? 0 : item.id;
    }
}
