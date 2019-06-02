using System.Collections.Generic;
using System.Globalization;
using Script.GlobalsScript;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    public Transform Portal;
    public Transform Item;

    public PortalData portal;
    public Weapons[] assassinW;
    public Weapons[] mageW;
    public Weapons[] archerW;
    public Weapons[] warriorW;
    
    
    public void End(IEventArgs args)
    {
        StaticData.ActualFloor = -1000;
        
        SpawnItem();
        SpawnEnd();
        UnlockNextLevel();
    }
    
    private void SpawnItem()
    {
        Weapons[] weaponses;
        
        if (StaticData.PlayerChoice == "Assassin") weaponses = assassinW;
        else if (StaticData.PlayerChoice == "Mage") weaponses = mageW;
        else if (StaticData.PlayerChoice == "Archer") weaponses = archerW;
        else weaponses = warriorW;
        
        Weapons w1 = ScriptableObject.CreateInstance<Weapons>();
        w1.Create(weaponses[Random.Range(0, weaponses.Length)], StaticData.RandomLevel());
        
        Weapons w2 = ScriptableObject.CreateInstance<Weapons>();
        w2.Create(weaponses[Random.Range(0, weaponses.Length)], StaticData.RandomLevel());
        
        Weapons w3 = ScriptableObject.CreateInstance<Weapons>();
        w3.Create(weaponses[Random.Range(0, weaponses.Length)], StaticData.RandomLevel());

        Instantiate(Item, new Vector3(8, 12, 0), Quaternion.identity).GetComponent<WeaponManager>().Create(w1);
        Instantiate(Item, new Vector3(10, 12, 0), Quaternion.identity).GetComponent<WeaponManager>().Create(w2);
        Instantiate(Item, new Vector3(12, 12, 0), Quaternion.identity).GetComponent<WeaponManager>().Create(w3);
    }

    private void SpawnEnd()
    {
        Instantiate(Portal, new Vector3(10, 15, 0), Quaternion.identity).GetComponent<Portal>().Create(portal, Vector3.zero);
    }

    private void UnlockNextLevel()
    {
        GlobalSave save = SaveSystem.LoadGlobal();
        
        List<string> text = new List<string>()
        {
            "1-5", "5-10", "10-15", "15-20", "20-25", "25-30","30-35","35-40","40-45","45-50","50-60","60-70","70-80","80-90","90-100"
        };

        string test = StaticData.LevelMap[0] + "-" + StaticData.LevelMap[1];
        int index = text.IndexOf(test) + 1;
        Debug.Log(index);
        Debug.Log(save.maxLvl);

        if (index != 0 && index >= save.maxLvl && index < 15)
        {
            GlobalInfo.MaxLvl = index + 1;
            SaveSystem.SaveGlobal();
        }
    }
}
