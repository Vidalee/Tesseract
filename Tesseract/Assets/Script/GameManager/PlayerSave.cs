using System;

[Serializable]
public class PlayerDataSave
{
    public int _MaxHp;
    public int _PhysicsDamage;
    public int _MagicDamage;
    public float _MoveSpeed;
    public int size;
    
    public bool[] _Unlock;
    public float[] _Cooldown;
    public float[] _Speed;
    public int[] _Damage;
    
    //public CompetencesData[] _Competences;

    public PlayerDataSave(PlayerData player)
    {
        _MaxHp = player.MaxHp;
        _PhysicsDamage = player.PhysicsDamage;
        _MagicDamage = player.MagicDamage;
        _MoveSpeed = player.MoveSpeed;
        size = player.Competences.Length;
        
        _Unlock = new bool[size];
        _Cooldown = new float[size];
        _Speed = new float[size];
        _Damage = new int[size];
        CompetenceSet(player);
    }

    private void CompetenceSet(PlayerData playerData)
    {
        for (int i = 0; i < playerData.Competences.Length; i++)
        {
            _Unlock[i] = playerData.Competences[i].Unlock;
            _Cooldown[i] = playerData.Competences[i].Cooldown;
            _Speed[i] = playerData.Competences[i].Speed;
            _Damage[i] = playerData.Competences[i].Damage;
        }
    }
}
