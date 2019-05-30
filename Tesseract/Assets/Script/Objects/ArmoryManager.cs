using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ArmoryManager : MonoBehaviour
{
    [SerializeField] private List<Weapons> archerWeapons;
    [SerializeField] private List<Weapons> assassinWeapons;
    [SerializeField] private List<Weapons> mageWeapons;
    [SerializeField] private List<Weapons> warriorWeapons;
    [SerializeField] protected GameObject weapon;
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
            newWeapon.transform.parent = transform;
        }
        newWeapon.GetComponent<WeaponManager>().Create(newWeaponData);
    }
}
