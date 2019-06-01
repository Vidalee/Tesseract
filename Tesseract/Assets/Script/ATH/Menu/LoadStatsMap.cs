using System.IO;
using Script.GlobalsScript;
using UnityEngine;
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

        StaticData.NumberFloor = 1 + StaticData.RandomLevel() / 5;

        ChangeScene.ChangeToScene("Dungeon");
    }

    public void ReturnChampSelect()
    {
        StaticData.Reset();
        GlobalInfo.Reset();
        
        ChangeScene.ChangeToScene("MenuPlayer");
    }

    public void Reset()
    {
        string path = Application.persistentDataPath + "/lvl.txt";
        if(File.Exists(path)) File.Delete(path);

        ChangeScene.ChangeToScene("LevelSelection");
    }
}
