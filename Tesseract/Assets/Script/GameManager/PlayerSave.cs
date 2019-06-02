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
    public int[] Lvl;
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
        ManaRegen = player.ManaRegen;
        Lvl = new int[5];

        Lvl[0] = player.Lvl;
        for (int i = 1; i < 4; i++)
        {
            Lvl[i] = player.Competences[i].Lvl;
        }
        
        weapon = ItemNull(player.Inventory.Weapon);
        weaponLvl = weapon == 0 ? 0 : player.Inventory.Weapon.Lvl;
    }

    private int ItemNull(GamesItem item)
    {
        return item == null ? 0 : item.id;
    }
}
