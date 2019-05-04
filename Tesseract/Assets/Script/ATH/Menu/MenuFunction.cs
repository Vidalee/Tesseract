using Script.GlobalsScript;
using UnityEngine;

public class MenuFunction : MonoBehaviour
{
    public void PlayScene(string scene)
    {
        ChangeScene.ChangeToScene(scene);
    }

    public void ChoosePlayer(string player)
    {
        StaticData.PlayerChoice = player;
    }
}
