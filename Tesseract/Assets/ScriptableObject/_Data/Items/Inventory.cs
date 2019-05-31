using Script.GlobalsScript.Struct;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] protected Weapons weapon;
    [SerializeField] protected Potions[] potions;
    public GameEvent PotionsAth;

    public Potions[] Potions
    {
        get => potions;
        set => potions = value;
    }
    
    public Potions UsePotion(int index)
    {
        if (potions[index] == null) return null;
        Potions pot = potions[index];
        potions[index] = null;

        PotionsAth.Raise(new EventArgsPotAth(null, index));
        return pot;
    }

    private bool AddPotion(Potions potion)
    {
        int index = 0;
        
        if (potions[0] == null) index = 0;
        else if (potions[1] == null) index = 1;
        else if (potions[2] == null) index = 2;
        else if (potions[3] == null) index = 3;
        else return false;

        potions[index] = potion;
        PotionsAth.Raise(new EventArgsPotAth(potion, index));
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
            case null:
                return false;
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
