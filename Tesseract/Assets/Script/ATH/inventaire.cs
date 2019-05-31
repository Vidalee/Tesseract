using Script.GlobalsScript;
using Script.GlobalsScript.Struct;
using UnityEngine;

public class inventaire : MonoBehaviour
{
    private PlayerData PlayerData;

    private PlayerManager Playermanager;
    
    public void PotionsAth(IEventArgs args)
    {
        EventArgsPotAth potAth = (EventArgsPotAth) args;
    }
}
