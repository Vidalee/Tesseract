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
        int lvl = Random.Range(int.Parse(value[0]), int.Parse(value[1]));
        
        StaticData.LevelMap = lvl;
        StaticData.NumberFloor = 3 + lvl / 5;

        ChangeScene.ChangeToScene("Dungeon");
    }
}
