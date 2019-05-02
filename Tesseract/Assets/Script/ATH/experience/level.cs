using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level : MonoBehaviour
{
    public Text txt_level;
    
    public PlayerData playerData;

    public PlayerManager Playermanager; 
    
    // Update is called once per frame
    
    void Start()
    { 
        playerData = Playermanager.GetComponent<PlayerManager>().PlayerData;
    }
    
    void Update()
    {
        txt_level.text = "Level : " + playerData.Lvl; 
    }
}
