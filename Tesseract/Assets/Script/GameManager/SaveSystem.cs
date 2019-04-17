using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(PlayerData player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.txt";
        FileStream stream =  new FileStream(path, FileMode.Create);
        
        PlayerDataSave data = new PlayerDataSave(player);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerDataSave LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.txt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerDataSave data = formatter.Deserialize(stream) as PlayerDataSave ;
            stream.Close();
            
            return data;
        }
        Debug.Log("No path for the save");
        return null;
    }
}
