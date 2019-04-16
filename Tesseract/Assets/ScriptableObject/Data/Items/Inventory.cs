using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] protected Weapons weapon;
    [SerializeField] protected List<Potions> potions;
    [SerializeField] protected int maxPotion;

    public Potions UsePotion(int index)
    {
        if (potions.Count <= index) return null;
        if (potions[index] == null) return null;
        
        Potions pot = potions[index];
        potions.RemoveAt(index);
        return pot;
    }

    public bool AddPotion(Potions potion)
    {
        if(potions.Count >= maxPotion) return false;
        
        potions.Add(potion);
        return true;
    }
}
