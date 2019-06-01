using UnityEngine;
using UnityEngine.UI;

public class PlayerLvlMenu : MonoBehaviour
{
    private void Start()
    {
        LoadLvl();
    }

    public void LoadLvl()
    {
        string lvl = "0";
        
        PlayerDataSave data = SaveSystem.LoadPlayer(transform.parent.name);
        if (data != null)
        {
            lvl = data.Lvl.ToString();
        }
        
        GetComponent<Text>().text = "Level : " + lvl;
    }
}
