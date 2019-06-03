using Script.GlobalsScript;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunction : MonoBehaviour
{
    public void PlayScene(string scene)
    {
        if (scene == "Login") StaticData.ActualFloor = 0;
        SceneManager.LoadScene(scene);
    }

    public void ChoosePlayer(string player)
    {
        StaticData.PlayerChoice = player;
    }

    public void StartMultiGame()
    {
        UDPRoomManager._socket.Send("PINFO START");
        SceneManager.LoadScene("MultiGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
