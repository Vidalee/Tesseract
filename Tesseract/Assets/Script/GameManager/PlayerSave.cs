using System;

[Serializable]
public class PlayerDataSave
{
    public int MaxHp;
    public int Hp;
    public int MaxMana;
    public int Mana;
    public int PhysicsDamage;
    public int MagicDamage;
    public float MoveSpeed;
    public int Xp;
    public int MaxXp;
    public int Lvl;
    public int ManaRegen;

    public float[] CompSpeed;
    public float[] CompCd;
    public int[] CompDamage;
    public int[] CompLive;
    public int[] CompNumber;
    
    public int weapon;


    public PlayerDataSave(PlayerData player)
    {
        MaxHp = player.MaxHp;
        Hp = player.Hp;
        MaxMana = player.MaxMana;
        Mana = player.Mana;
        PhysicsDamage = player.PhysicsDamage;
        MagicDamage = player.MagicDamage;
        MoveSpeed = player.MoveSpeed;
        Xp = player.Xp;
        MaxXp = player.MaxXp;
        Lvl = player.Lvl;
        ManaRegen = player.ManaRegen;

        int length = player.Competences.Length;
        
        CompCd = new float[length];
        CompSpeed = new float[length];
        CompDamage = new int[length];
        CompLive = new int[length];
        CompNumber = new int[length];
        
        for (int i = 0; i < length; i++)
        {
            CompetencesData c = player.Competences[i];
            
            CompSpeed[i] = c.Speed;
            CompCd[i] = c.Cooldown;
            CompDamage[i] = c.Damage;
            CompLive[i] = c.Live;
            CompNumber[i] = c.Number;
        }

        weapon = ItemNull(player.Inventory.Weapon);
    }

    private int ItemNull(GamesItem item)
    {
        return item == null ? 0 : item.id;
    }
}
