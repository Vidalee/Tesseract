using System.IO;
using Script.GlobalsScript;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadStatsMap : MonoBehaviour
{
    private void Start()
    {
        if(!transform.parent.name.Contains("M"))
            GetComponentInChildren<Text>().text = "Level : " + transform.parent.name;
    }

    public void SetMapLevel()
    {
        string[] value = transform.parent.name.Split('-');
        
        StaticData.LevelMap[0] = int.Parse(value[0]);
        StaticData.LevelMap[1] = int.Parse(value[1]);

        StaticData.NumberFloor = 1 + StaticData.RandomLevel() / 10;

        SceneManager.LoadScene("Dungeon");
    }

    public void ReturnChampSelect()
    {
        StaticData.Reset();
        GlobalInfo.Reset();
        
        SceneManager.LoadScene("MenuPlayer");
    }

    public void Reset()
    {
        string path = Application.persistentDataPath + "/lvl.txt";
        if(File.Exists(path)) File.Delete(path);

        SceneManager.LoadScene("LevelSelection");
    }
}
