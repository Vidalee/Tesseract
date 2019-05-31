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

        _weapons = GetComponentInChildren<WeaponsAth>();
        _otherWeapons = GetComponentInChildren<WeaponsAth>();

        _potions = GetComponentsInChildren<PotionsAth>();
    }

    private void Update()
    {
        if (!wait && Input.GetKey(KeyCode.M))
        {
            wait = true;
            Canvas.enabled = false;
        }
        
        if(Input.GetKey(KeyCode.C)) AddPotion(new EventArgsPotAth(null, 0));
        if(Input.GetKey(KeyCode.V)) AddPotion(new EventArgsPotAth(null, 1));
        if(Input.GetKey(KeyCode.B)) AddPotion(new EventArgsPotAth(null, 2));
        if(Input.GetKey(KeyCode.N)) AddPotion(new EventArgsPotAth(null, 3));
    }

    public void AddPotion(IEventArgs potion)
    {
        EventArgsPotAth potAth = potion as EventArgsPotAth;
        Potions pot = potAth.Pot;
        int id = potAth.Id;

        _potions[id].SetPotion(pot);
    }
}
