using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp : MonoBehaviour
{
    public Text txt_sante;
    
    public PlayerData playerData;
    
    // Update is called once per frame
    void Update()
    {
        txt_sante.text = playerData.Hp + ""; 
    }
}
