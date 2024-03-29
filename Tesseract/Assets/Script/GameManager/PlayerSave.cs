﻿using System;
using UnityEngine;

[Serializable]
public class PlayerDataSave
{
    public int[] Lvl;

    public long Xp;
    public int weapon;
    public int weaponLvl;
    public int CompPoint;

    public PlayerDataSave(PlayerData player)
    {
        Xp = player.Xp;
        Lvl = new int[5];
        CompPoint = player.CompPoint;

        Lvl[0] = player.Lvl;
        for (int i = 1; i < 5; i++)
        {
            Lvl[i] = player.Competences[i - 1].Lvl;
            Debug.Log(Lvl[i]);
        }
        
        weapon = ItemNull(player.Inventory.Weapon);
        weaponLvl = weapon == 0 ? 0 : player.Inventory.Weapon.Lvl;
    }

    private int ItemNull(GamesItem item)
    {
        return item == null ? 0 : item.id;
    }
}
