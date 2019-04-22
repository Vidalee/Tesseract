using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] protected Weapons weapon;
    [SerializeField] protected List<Potions> potions;
    [SerializeField] protected int maxPotion;
    public GameEvent PotionsAth; 

    public List<Potions> Potions => potions;

    public Potions UsePotion(int index)
    {
        if (potions.Count == 0) return null;
        Potions pot = potions[index];
        PotionsAth.Raise(new EventArgsPotAth(index, null));
        potions.RemoveAt(index);
        return pot;
    }

    private bool AddPotion(Potions potion)
    {
        if(potions.Count >= maxPotion) return false;
        PotionsAth.Raise(new EventArgsPotAth(potions.Count, potion.icon));
        potions.Add(potion);
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
}
