using System.IO;
using UnityEngine;

public class DeletePlayer : MonoBehaviour
{
    public void DeleteSave(string name)
    {
        string path = Application.persistentDataPath + "/" + name + ".txt";

        if(File.Exists(path)) File.Delete(path);
        
        GameObject.Find(name).GetComponentInChildren<PlayerLvlMenu>().LoadLvl();
    }
}
