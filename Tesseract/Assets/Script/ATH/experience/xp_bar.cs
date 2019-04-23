using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xp_bar : MonoBehaviour
{
    public GameObject Playermanager;

    public PlayerData PlayerData;

    private float XpPercent;
    
    // Start is called before the first frame update
    void Start()
    { 
        PlayerData = Playermanager.GetComponent<PlayerManager>().GetPlayerData;
    }
 
    // Update is called once per frame
    void Update() 
    {
        XpPercent = (float) PlayerData.Xp / PlayerData.MaxXp;
        transform.localScale = new Vector3(XpPercent, transform.localScale.y);
    }
}
