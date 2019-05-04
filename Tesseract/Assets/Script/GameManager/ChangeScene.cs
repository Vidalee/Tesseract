using UnityEngine.SceneManagement;

public static class ChangeScene 
{
    public static void ChangeToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
