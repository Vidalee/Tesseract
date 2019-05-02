using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hp_bar : MonoBehaviour
{
    public GameObject Playermanager;

    public PlayerData PlayerData;

    private float HpPercent;
    
    // Start is called before the first frame update
    void Start()
    { 
        PlayerData = Playermanager.GetComponent<PlayerManager>().PlayerData;
    }
 
    // Update is called once per frame
    void Update() 
    {
        HpPercent = (float) PlayerData.Hp / PlayerData.MaxHp;
        transform.localScale = new Vector3(HpPercent, transform.localScale.y);
    }
}
