using System.Collections;
using Script.GlobalsScript.Struct;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] protected Weapons weapon;
    [SerializeField] protected Potions[] potions;
    
    public GameEvent PotionsAth;
    public GameEvent WeaponsAth;
    public Transform P;
    public Transform W;

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
        int index;
        
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
        if (weapon == null && weapons != null)
        {
            WeaponsAth.Raise(new EventArgsWeaponsAth(weapons));
            weapon = weapons;
            return true;
        }

        return false;
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
    
    public void SetAth()
    {        
        WeaponsAth.Raise(new EventArgsWeaponsAth(weapon));

        for (int i = 0; i < 4; i++)
        {
            PotionsAth.Raise(new EventArgsPotAth(potions[i], i));
        }
    }
    
    public Weapons Weapon
    {
        get => weapon;
        set => weapon = value;
    }

    public void RemoveWeapon(Vector3 pos)
    {
        if(weapon == null) return;
        
        Transform o = Instantiate(W, pos, Quaternion.identity);
        o.GetComponent<WeaponManager>().Create(weapon);
        
        weapon = null;
        WeaponsAth.Raise(new EventArgsWeaponsAth(null));
    }
    
    public void RemovePotion(int id, Vector3 pos)
    {
        if (potions[id] == null) return;
        Transform o = Instantiate(P, pos, Quaternion.identity);
        o.GetComponent<PotionManager>().Create(potions[id]);
        
        potions[id] = null;
        PotionsAth.Raise(new EventArgsPotAth(null, id));
    }
}
