using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoryManager : MonoBehaviour
{
    [SerializeField] private List<Weapons> archerWeapons;
    [SerializeField] private List<Weapons> assassinWeapons;
    [SerializeField] private List<Weapons> mageWeapons;
    [SerializeField] private List<Weapons> warriorWeapons;

    public Weapons GetWeapon(string category)
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
}
