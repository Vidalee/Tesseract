using Script.GlobalsScript;
using UnityEngine;
using UnityEngine.UI;

public class LoadStatsMap : MonoBehaviour
{
    private void Start()
    {
        GetComponentInChildren<Text>().text = "Level : " + transform.parent.name;
    }

    public void SetMapLevel()
    {
        string[] value = transform.parent.name.Split('-');
        
        StaticData.LevelMap[0] = int.Parse(value[0]);
        StaticData.LevelMap[1] = int.Parse(value[1]);

        StaticData.NumberFloor = 3 + StaticData.RandomLevel() / 5;

        ChangeScene.ChangeToScene("Dungeon");
    }
}
