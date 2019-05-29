using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mana_bar : MonoBehaviour
{
    public GameObject Playermanager;

    public PlayerData PlayerData;

    private float ManaPercent;

    // Start is called before the first frame update
    void Start()
    {
        PlayerData = Playermanager.GetComponent<PlayerManager>().GetPlayerData;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.gameObject.activeSelf)
        {
            ManaPercent = (float) PlayerData.Mana / PlayerData.MaxMana;
            transform.localScale = new Vector3(ManaPercent, transform.localScale.y);
        }
    }
}
