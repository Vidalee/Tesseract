using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Variable


    [SerializeField] protected PlayerData[] _PlayersData;
    [SerializeField] protected PlayerData[] _PlayersDataCopy;
    public GamesItem[] Items;
    public GameEvent playerXp;
    public GameEvent playerMaxXp;
    public GameEvent playerLvl;
    
    public string choice;
        
    public Transform Player;
    private PlayerData _playerData;

    private MapData _mapData;

    public PlayerData PlayerData => _playerData;

    #endregion

    public PlayerData GetPlayerData => PlayerData;

    #region Update

    private void Update()
    {
        if(Input.GetKey("k")) SavePlayer();
        if(Input.GetKey("l")) LoadPlayer();
        if(Input.GetKey("x")) GetXp(10);
    }

    #endregion

    #region Initialise

    private void Awake()
    {
        
        ResetStat(FindClass());
        
        InstantiatePlayer();
    }

    public void Create(PlayerData playerData, MapData mapData)
    {
        _mapData = mapData;
        _playerData = playerData;
        InstantiatePlayer();
    }
    
    private void InstantiatePlayer()
    {
        Vector3 pos = new Vector3(10, 10, 0);

        /*
        int roomsNumber = _mapData.RoomsData.Length;

        if (roomsNumber != 0)
        {
            int rand = Random.Range(0, roomsNumber);
            RoomData room = _mapData.RoomsData[rand];
        
            int xRand = Random.Range(0, room.Width);
            int yRand = Random.Range(0, room.Height);

            while (room.GridObstacles[xRand, yRand])
            {
                xRand = Random.Range(0, room.Width);
                yRand = Random.Range(0, room.Height);
            }

            pos.x = xRand;
            pos.y = yRand;
        }
        */
        Transform o = Instantiate(Player, pos, Quaternion.identity, transform);
        
        o.GetComponent<PlayerMovement>().Create(_playerData);
        o.GetComponent<PlayerDash>().Create(_playerData);
        o.GetComponent<PotionUsable>().Create(_playerData);
        o.GetComponent<PlayerAttack>().Create(_playerData);
        o.GetComponent<Live>().Create(_playerData);
        o.GetComponentInChildren<PlayerAnimation>().Create(_playerData);
    }

    #endregion

    #region Player save and load

    private int FindClass()
    {
        int index = 0;
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
    
    private void ResetStat(int index)
    {
        _playerData = _PlayersData[index];
        _playerData.MaxHp = _PlayersDataCopy[index].MaxHp;
        _playerData.Hp = _PlayersDataCopy[index].Hp;
        _playerData.MaxMana = _PlayersDataCopy[index].MaxMana;
        _playerData.Mana = _PlayersDataCopy[index].Mana;
        _playerData.PhysicsDamage = _PlayersDataCopy[index].PhysicsDamage;
        _playerData.MagicDamage = _PlayersDataCopy[index].MagicDamage;
        _playerData.MoveSpeed = _PlayersDataCopy[index].MoveSpeed;
        _playerData.Xp = _PlayersDataCopy[index].Xp;
        _playerData.MaxXp = _PlayersDataCopy[index].MaxXp;
        _playerData.TotalXp = _PlayersDataCopy[index].TotalXp;
        _playerData.Lvl = _PlayersDataCopy[index].Lvl;
        for (int i = 0; i < _playerData.Competences.Length; i++)
        {
            CompetencesData c = _playerData.Competences[i];
            CompetencesData cc = _PlayersDataCopy[index].Competences[i];

            c.Speed = cc.Speed;
            c.Cooldown = cc.Cooldown;
            c.Damage = cc.Damage;
            c.Live = cc.Live;
            c.Number = cc.Number;
        }
    }

    private void SavePlayer()
    {
        //Debug.Log("Save");
        SaveSystem.SavePlayer(_playerData);
    }

    private void LoadPlayer()
    {
        //Debug.Log("load");
        PlayerDataSave data = SaveSystem.LoadPlayer(_playerData.Name);
        
        ResetStat(FindClass());
        GetXp(data.xp);

        _playerData.Inventory.AddItem(FindItems(data.weapon));
        
        _playerData.Inventory.AddItem(FindItems(data.inventory[0]));
        _playerData.Inventory.AddItem(FindItems(data.inventory[1]));
        _playerData.Inventory.AddItem(FindItems(data.inventory[2]));
        _playerData.Inventory.AddItem(FindItems(data.inventory[3]));
    }

    private GamesItem FindItems(int id)
    {
        foreach (var it in Items)
        {
            if (it.id == id) return it;
        }

        return null;
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
            _playerData.TotalXp += gap;
            
            _playerData.MaxXp = (int) (_playerData.MaxXp * 1.1f);
            
            UpgradeCompetence();
            UpgradeStats();
            
            gap = _playerData.MaxXp - _playerData.Xp;
            
            playerLvl.Raise(new EventArgsInt(_playerData._Lvl));
            playerMaxXp.Raise(new EventArgsInt(_playerData.MaxXp));
        }
        
        _playerData.Xp += amout;
        _playerData.TotalXp += amout;
        playerXp.Raise(new EventArgsInt(_playerData.Xp));
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
