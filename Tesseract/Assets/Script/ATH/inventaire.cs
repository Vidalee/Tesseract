using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class inventaire : MonoBehaviour
{
    private PlayerData PlayerData;

    public GameObject Panel; 

    private PlayerManager Playermanager;

    private Image[] slot; 
    
    // Start is called before the first frame update
    void Start()
    {
        //PlayerData = Playermanager.GetComponent<PlayerManager>().GetPlayerData;
    }

    public void PotionsAth(IEventArgs args)
    {
        EventArgsPotAth potAth = (EventArgsPotAth) args;
    }
}
