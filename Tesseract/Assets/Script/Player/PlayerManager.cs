using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] protected PlayerData PlayerData;


    private void Update()
    {
        if(Input.GetKey("k")) SavePlayer();
        if(Input.GetKey("l")) LoadPlayer();
    }

    public void SavePlayer()
    {
        Debug.Log("Save");
        SaveSystem.SavePlayer(PlayerData);
    }

    public void LoadPlayer()
    {
        Debug.Log("load");
        PlayerDataSave data = SaveSystem.LoadPlayer();

        PlayerData.MaxHp = data._MaxHp;
        PlayerData.PhysicsDamage = data._PhysicsDamage;
        PlayerData.MagicDamage = data._MagicDamage;
        PlayerData.MoveSpeed = data._MoveSpeed;
        SetCompetence(data);
    }

    private void SetCompetence(PlayerDataSave data)
    {
        for (int i = 0; i < data.size; i++)
        {
            PlayerData.Competences[i].Unlock = data._Unlock[i];
            PlayerData.Competences[i].Cooldown = data._Cooldown[i];
            PlayerData.Competences[i].Speed = data._Speed[i];
            PlayerData.Competences[i].Damage = data._Damage[i];
        }
    }
}
