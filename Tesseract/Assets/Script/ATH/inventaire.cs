using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Script.GlobalsScript;
using Script.GlobalsScript.Struct;
using UnityEngine;
using UnityEngine.UI;

public class inventaire : MonoBehaviour
{
    private PlayerData PlayerData;

    public GameObject Panel;

    private PlayerManager Playermanager;

    private Image[] slot;
    
    public void PotionsAth(IEventArgs args)
    {
        EventArgsPotAth potAth = (EventArgsPotAth) args;
    }
}
