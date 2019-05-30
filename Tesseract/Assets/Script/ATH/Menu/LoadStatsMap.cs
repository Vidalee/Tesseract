using Script.GlobalsScript;
using UnityEngine;

public class LoadStatsMap : MonoBehaviour
{
    public void SetMapLevel()
    {
        string[] value = transform.parent.name.Split('-');
        int lvl = Random.Range(int.Parse(value[0]), int.Parse(value[1]));
        
        StaticData.LevelMap = lvl;
        StaticData.NumberFloor = 3 + lvl / 5;
    }
}
