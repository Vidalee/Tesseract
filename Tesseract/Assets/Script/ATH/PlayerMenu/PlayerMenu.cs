using Script.GlobalsScript;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    public void NextScene()
    {
        StaticData.PlayerChoice = gameObject.name;
        ChangeScene.ChangeToScene("Map");
    }
}
