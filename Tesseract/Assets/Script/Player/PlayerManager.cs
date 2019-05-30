using Script.GlobalsScript;
using Script.GlobalsScript.Struct;
using Script.Pathfinding;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{
    #region Variable

    public GameEvent SetXpBar;
    public GameEvent SetXp;
    public GameEvent SetHpBar;
    public GameEvent SetHp;
    public GameEvent SetManaBar;
    public GameEvent SetMana;
    public GameEvent SetLvl;
    
    [SerializeField] protected PlayerData[] _PlayersData;
    [SerializeField] protected PlayerData[] _PlayersDataCopy;
    public GamesItem[] Items;
    
    public string Perso;
        
    public Transform Player;
    private PlayerData _playerData;

    private MapData _mapData;
    private TileData _miniMap;

    #endregion

    public PlayerData PlayerData => _playerData;

    #region Update

    private void Update()
    {
        if(Input.GetKey("k")) SavePlayer();
        if(Input.GetKey("x")) GetXp(10);
    }

    #endregion

    #region Initialise

    public void Create(int x, int y)
    {
        if (StaticData.ActualFloor == 0)
        {
            StaticData.ActualFloor = 1;
            string type = Perso != "" ? Perso : StaticData.PlayerChoice;
            int pers = FindClass(type);

            _playerData = _PlayersData[pers];
            LoadPlayer();
        
            InstantiatePlayer(x, y);
        }
        else
        {
            _playerData = StaticData.actualData;
            InstantiatePlayer(x, y);
        }
    }
    
    private void InstantiatePlayer(int x, int y)
    {
        Transform o = Instantiate(Player, new Vector3(x, y, 0), Quaternion.identity, transform);
        
        SetXp.Raise(new EventArgsString(_playerData.Xp.ToString()));
        SetXpBar.Raise(new EventArgsFloat((float) _playerData.Xp / _playerData.MaxXp));
        SetHp.Raise(new EventArgsString(_playerData.Hp.ToString()));
        SetHpBar.Raise(new EventArgsFloat((float) _playerData.Hp / _playerData.MaxHp));
        SetMana.Raise(new EventArgsString(_playerData.Mana.ToString()));
        SetManaBar.Raise(new EventArgsFloat((float) _playerData.Mana / _playerData.MaxMana));
        SetLvl.Raise(new EventArgsString(_playerData.Lvl.ToString()));
        
        o.GetComponent<PlayerMovement>().Create(_playerData);
        o.GetComponent<PlayerDash>().Create(_playerData);
        o.GetComponent<PotionUsable>().Create(_playerData);
        o.GetComponent<PlayerAttack>().Create(_playerData);
        o.GetComponent<Live>().Create(_playerData);
        o.GetComponentInChildren<PlayerAnimation>().Create(_playerData);
        
        AllNodes.players.Add(o);
        GenerateEnemies.players.Add(o);
    }

    #endregion

    #region Player save and load

    private int FindClass(string choice)
    {
        int index = 2;
        switch (choice)
        {
            case "Archer":
                index = 0;
                break;
            case "Assassin":
                index = 1;
                break;
            case "Mage":
                index = 2;
                break;
            case "Warrior":
                index = 3;
                break;
            default:
                Debug.Log("???");
                break;
        }

        return index;
    }

    private void SavePlayer()
    {
        SaveSystem.SavePlayer(_playerData);
    }

    private void LoadPlayer()
    {
        
        PlayerDataSave data = SaveSystem.LoadPlayer(_playerData.Name);
        if (data == null || data.CompCd == null || data.CompCd.Length == 0)
        {
            ResetStats(FindClass(_playerData.Name));
            return;
        }

        LoadStats(data);
        
        _playerData.Inventory.AddItem(FindItems(data.weapon));
        
        _playerData.Inventory.Potions = new Potions[4];
    }

    private void ResetStats(int index)
    {
        _playerData = _PlayersData[index];
        _playerData.MaxHp = _PlayersDataCopy[index].MaxHp;
        _playerData.Hp = _PlayersDataCopy[index].Hp;
        _playerData.MaxMana = _PlayersDataCopy[index].MaxMana;
        _playerData.Mana = _PlayersDataCopy[index].Mana;
        _playerData.PhysicsDamage = _PlayersDataCopy[index].PhysicsDamage;
        _playerData.MagicDamage = _PlayersDataCopy[index].MagicDamage;
        _playerData.MoveSpeed = _PlayersDataCopy[index].MoveSpeed;
        _playerData.MaxXp = _PlayersDataCopy[index].MaxXp;
        _playerData.Xp = _PlayersDataCopy[index].Xp;
        _playerData.Lvl = _PlayersDataCopy[index].Lvl;
        _playerData.ManaRegen = _PlayersDataCopy[index].ManaRegen;
        
        for (int i = 0; i < _playerData.Competences.Length; i++)
        {
            CompetencesData c = _playerData.Competences[i];
            CompetencesData cc = _PlayersDataCopy[index].Competences[i];

            c.Speed = cc.Speed;
            c.Cooldown = cc.Cooldown;
            c.Damage = cc.Damage;
            c.Live = cc.Live;
            c.Number = cc.Number;
            c.ManaCost = cc.ManaCost;
        }
    }

    private void LoadStats(PlayerDataSave data)
    {
        _playerData.MaxHp = data.MaxHp;
        _playerData.Hp = data.MaxHp;
        _playerData.MaxMana = data.MaxMana;
        _playerData.Mana = data.MaxMana;
        _playerData.PhysicsDamage = data.PhysicsDamage;
        _playerData.MagicDamage = data.MagicDamage;
        _playerData.MoveSpeed = data.MoveSpeed;
        _playerData.Xp = data.Xp;
        _playerData.MaxXp = data.MaxXp;
        _playerData.Lvl = data.Lvl;
        _playerData.ManaRegen = data.ManaRegen;
        
        for (int i = 0; i < _playerData.Competences.Length; i++)
        {
            CompetencesData c = _playerData.Competences[i];

            c.Speed = data.CompSpeed[i];
            c.Cooldown = data.CompCd[i];
            c.Damage = data.CompDamage[i];
            c.Live = data.CompLive[i];
            c.Number = data.CompNumber[i];
            c.ManaCost = data.CompManaCost[i];
        }
    }

    private GamesItem FindItems(int id)
    {
        foreach (var it in Items)
        {
            if (it == null) return null;
            if (it.id == id) return it;
        }

        return null;
    }

    public void GetXp(IEventArgs args)
    {
        GetXp(((EventArgsInt) args).X);
    }
    
    #endregion

    #region PlayerStats

    public void GetXp(int amout)
    {
        int gap = _playerData.MaxXp - _playerData.Xp;

        while (amout >= gap)
        {
            amout = amout - gap;

            if (_playerData.Lvl < _playerData.MaxLvl) _playerData.Lvl++;
            _playerData.Xp = gap;
            
            _playerData.MaxXp = (int) (_playerData.MaxXp * 1.1f);
            
            UpgradeCompetence();
            UpgradeStats();
            
            gap = _playerData.MaxXp - _playerData.Xp;
        }
        
        _playerData.Xp += amout;
    }

    #endregion

    #region LvlUp

    private void UpgradeStats()
    {
        if (_playerData.Lvl % 5 == 0)
        {
            _playerData.MaxHp += 10;
            _playerData.Hp = _playerData.MaxHp;
            _playerData.MaxMana += 10;
            _playerData.Mana = _playerData.MaxMana;
            _playerData.MoveSpeed *= 1.05f;
            _playerData.ManaRegen++;
        }

        _playerData.PhysicsDamage++;
        _playerData.MagicDamage++;
    }

    private void UpgradeCompetence()
    {
        CompetenceTree.CompetenceLvlUp(_playerData.Competences, _playerData.Lvl);
    }

    #endregion

    public void AddItem(IEventArgs item)
    {
        _playerData.Inventory.AddItem((item as EventArgsItem).Item);
    }
}
