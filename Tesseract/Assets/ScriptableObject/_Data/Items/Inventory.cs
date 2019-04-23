using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] protected Weapons weapon;
    [SerializeField] protected Potions[] potions;
    
    public Potions[] Potions => potions;
    public GameEvent PotionsAth; 

    public Potions UsePotion(int index)
    {
        if (potions[index] == null) return null;
        Potions pot = potions[index];
        potions[index] = null;

        PotionsAth.Raise(new EventArgsPotAth(index, null));
        return pot;
    }

    private bool AddPotion(Potions potion)
    {
        if (potions[0] == null) potions[0] = potion;
        else if (potions[1] == null) potions[1] = potion;
        else if (potions[2] == null) potions[2] = potion;
        else if (potions[3] == null) potions[3] = potion;
        else return false;

        PotionsAth.Raise(new EventArgsPotAth(potions.Length, potion.icon));
        return true;
    }

    private bool AddWeapons(Weapons weapons)
    {
        return true;
    }

    public bool AddItem(GamesItem item)
    {
        switch (item)
        {
            case Potions _:
                return AddPotion(item as Potions);
            case Weapons _:
                return AddWeapons(item as Weapons);
            default:
                return false;
        }
    }

    public Weapons Weapon
    {
        get => weapon;
        set => weapon = value;
    }
}
