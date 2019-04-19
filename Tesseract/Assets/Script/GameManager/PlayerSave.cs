using System;

[Serializable]
public class PlayerDataSave
{
    public string name;
    public int xp;
    
    //public CompetencesData[] _Competences;

    public PlayerDataSave(PlayerData player)
    {
        name = player.Name;
        xp = player.TotalXp;
    }
}
