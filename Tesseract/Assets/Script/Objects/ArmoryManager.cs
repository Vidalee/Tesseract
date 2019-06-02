using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Script.GlobalsScript;
using UnityEngine;

public class ArmoryManager : MonoBehaviour
{
    [SerializeField] public List<Weapons> archerWeapons;
    [SerializeField] public List<Weapons> assassinWeapons;
    [SerializeField] public List<Weapons> mageWeapons;
    [SerializeField] public List<Weapons> warriorWeapons;
    [SerializeField] public GameObject weapon;
    [SerializeField] public LayerMask defaultLayer;
    public Weapons GetWeaponData(string category)
    {
        switch (category)
        {
            case "Archer" :
                return archerWeapons[Random.Range(0, archerWeapons.Count)];
            case "Assassin" :
                return assassinWeapons[Random.Range(0, assassinWeapons.Count)];
            case "Mage" :
                return mageWeapons[Random.Range(0, mageWeapons.Count)];
            case "Warrior" :
                return warriorWeapons[Random.Range(0, warriorWeapons.Count)];
            default:
                Debug.Log("GetWeapon : not a class");
                return warriorWeapons[0];
        }
    }

    public void CreateWeapon(Weapons weaponData, Transform transform, int lvl = 1, Transform parent = null)
    {
        GameObject newWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        Weapons newWeaponData = ScriptableObject.CreateInstance<Weapons>();
        newWeaponData.Create(weaponData, lvl);
        if (parent != null)
        {
            newWeaponData.inPlayerInventory = true;
            newWeapon.transform.parent = parent;
            newWeapon.layer = defaultLayer;
        }
        newWeapon.GetComponent<WeaponManager>().Create(newWeaponData);
    }
}
