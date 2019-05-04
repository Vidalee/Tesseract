using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(PlayerData player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + player.Name + ".txt";
        FileStream stream =  new FileStream(path, FileMode.Create);
        PlayerDataSave data = new PlayerDataSave(player);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerDataSave LoadPlayer(string name)
    {
        string path = Application.persistentDataPath + "/" + name + ".txt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerDataSave data = formatter.Deserialize(stream) as PlayerDataSave ;
            stream.Close();
            
            return data;
        }
        return null;
    }
}
