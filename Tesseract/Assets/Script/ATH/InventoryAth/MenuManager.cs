using System.Collections;
using Script.GlobalsScript;
using Script.GlobalsScript.Struct;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private Canvas Canvas;
    
    private WeaponsAth _weapons;
    private WeaponsAth _otherWeapons;
    private PotionsAth[] _potions;
    
    private bool wait;

    private void Start()
    {
        Canvas = GetComponent<Canvas>();
        Canvas.enabled = false;

        _weapons = GetComponentInChildren<WeaponsAth>();
        _otherWeapons = GetComponentInChildren<WeaponsAth>();

        _potions = GetComponentsInChildren<PotionsAth>();
    }

    private void Update()
    {
        if (!wait && Input.GetKey(KeyCode.M))
        {
            Canvas.enabled = !Canvas.enabled;
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        wait = true;
        yield return new WaitForSeconds(0.5f);
        wait = false;
    }

    public void AddPotion(IEventArgs potion)
    {
        EventArgsPotAth potAth = potion as EventArgsPotAth;
        Potions pot = potAth.Pot;
        int id = potAth.Id;

        _potions[id].SetPotion(pot);
    }


    public void AddWeapons(IEventArgs weapons)
    {
        EventArgsWeaponsAth weaponsAth = weapons as EventArgsWeaponsAth;
        _weapons.SetWeapons(weaponsAth.Weapons);
    }
    
    public void AddOtherWeapons(IEventArgs weapons)
    {
        EventArgsWeaponsAth weaponsAth = weapons as EventArgsWeaponsAth;
        _otherWeapons.SetWeapons(weaponsAth.Weapons);
    }
}
