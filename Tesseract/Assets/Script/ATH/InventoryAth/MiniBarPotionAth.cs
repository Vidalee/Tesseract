using Script.GlobalsScript;
using Script.GlobalsScript.Struct;
using UnityEngine;
using UnityEngine.UI;

public class MiniBarPotionAth : MonoBehaviour
{
    private Image[] slot;
    public Sprite None;
    
    private void Awake()
    {
        slot = new Image[4];
        
        slot[0] = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        slot[1] = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        slot[2] = transform.GetChild(2).GetChild(0).GetComponent<Image>();
        slot[3] = transform.GetChild(3).GetChild(0).GetComponent<Image>();
    }

    public void SetPotion(IEventArgs args)
    {
        EventArgsPotAth potAth = args as EventArgsPotAth;
        slot[potAth.Id].sprite = potAth.Pot == null ? None : potAth.Pot.icon;
    }
}
