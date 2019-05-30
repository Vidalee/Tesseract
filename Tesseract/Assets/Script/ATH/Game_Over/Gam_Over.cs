using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gam_Over : MonoBehaviour
{
    public GameObject Canvas;

    public PlayerManager PlayerManager; 
    // Start is called before the first frame update
    void Start()
    {
        Canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.PlayerData.Hp == 0)
        {
            Time.timeScale = 0; 
            Canvas.SetActive(true);
        }
        
    }
}
