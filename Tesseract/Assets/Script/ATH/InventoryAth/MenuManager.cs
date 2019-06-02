using System.Collections;
using Script.GlobalsScript;
using Script.GlobalsScript.Struct;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private Canvas Canvas;
    
    private WeaponsAth _weapons;
    
    private GameObject _otherWeaponsT;
    private WeaponsAth _otherWeaponsA;
    
    private GameObject _otherPotionsT;
    private PotionsAth _otherPotionsA;

    private PotionsAth[] _potions;
    
    private bool wait;

    private void Awake()
    {
        Canvas = GetComponent<Canvas>();
        Canvas.enabled = false;
        Transform t = transform.GetChild(0);

        _otherPotionsT = t.GetChild(0).gameObject;
        _otherWeaponsT = t.GetChild(1).gameObject;
        _otherWeaponsA = _otherWeaponsT.GetComponent<WeaponsAth>();
        _otherPotionsA = _otherPotionsT.GetComponent<PotionsAth>();
        
        _weapons = t.GetChild(3).GetComponentInChildren<WeaponsAth>();

        _potions = new PotionsAth[4];
        _potions[0] = t.GetChild(4).GetComponent<PotionsAth>();
        _potions[1] = t.GetChild(5).GetComponent<PotionsAth>();
        _potions[2] = t.GetChild(6).GetComponent<PotionsAth>();
        _potions[3] = t.GetChild(7).GetComponent<PotionsAth>();
        
        Clear();
    }

    private void Update()
    {
        if (!wait && Input.GetKey(KeyCode.E))
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
    
    public void AddOtherWeapons(IEventArgs itemArg)
     {
         GamesItem item = (itemArg as EventArgsItemAth).Item;
         
         if (item is Potions potions)
         {
             _otherPotionsT.SetActive(true);
             _otherWeaponsT.SetActive(false);
             
             _otherPotionsA.SetPotion(potions);
         }
         else if (item is Weapons weapons)
         {
             _otherPotionsT.SetActive(false);
             _otherWeaponsT.SetActive(true);
             
             _otherWeaponsA.SetWeapons(weapons);
         }
     }
    
    public void RemoveOtherWeapons(IEventArgs itemArg)
    {
        GamesItem item = (itemArg as EventArgsItemAth).Item;
        
        if (item is Potions potions && _otherPotionsA.Pot == potions)
        {
            _otherPotionsA.SetPotion(null);
            _otherPotionsT.SetActive(false);
        }
        else if (item is Weapons weapons && _otherWeaponsA.Weapons == weapons)
        {
            _otherWeaponsA.SetWeapons(null);
            _otherWeaponsT.SetActive(false);
        }
    }

    public void Clear()
    {
        _otherPotionsT.SetActive(false);
        _otherWeaponsT.SetActive(false);
    }
}
