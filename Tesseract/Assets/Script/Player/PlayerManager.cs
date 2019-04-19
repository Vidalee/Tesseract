using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Variable

    [SerializeField] protected PlayerData _playerData;
    private MapData _mapData;
    public Transform Player;

    #endregion

    #region Update

    private void Update()
    {
        //if(Input.GetKey("s")) SavePlayer();
        //if(Input.GetKey("l")) LoadPlayer();
    }


    #endregion

    #region Initialise

    private void Awake()
    {
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
        Vector3 pos = new Vector3(5, 5, 0);

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

    private void SavePlayer()
    {
        Debug.Log("Save");
        SaveSystem.SavePlayer(_playerData);
    }

    private void LoadPlayer()
    {
        Debug.Log("load");
        PlayerDataSave data = SaveSystem.LoadPlayer();

        _playerData.MaxHp = data._MaxHp;
        _playerData.PhysicsDamage = data._PhysicsDamage;
        _playerData.MagicDamage = data._MagicDamage;
        _playerData.MoveSpeed = data._MoveSpeed;
        SetCompetence(data);
    }

    private void SetCompetence(PlayerDataSave data)
    {
        for (int i = 0; i < data.size; i++)
        {
            _playerData.Competences[i].Unlock = data._Unlock[i];
            _playerData.Competences[i].Cooldown = data._Cooldown[i];
            _playerData.Competences[i].Speed = data._Speed[i];
            _playerData.Competences[i].Damage = data._Damage[i];
        }
    }

    #endregion

    #region PlayerStats

    public void GetXp(int amout)
    {
        int gap = _playerData.MaxXp - _playerData.Xp;

        if (amout >= gap)
        {
            int add = amout - gap;
            _playerData.Lvl++;
            _playerData.Xp = add;
            _playerData.MaxXp = (int) (_playerData.Xp * 1.1);
        }
        else
        {
            _playerData.Xp += amout;
        }
    }

    #endregion

    #region LvlUp

    private void UpgradeStats()
    {
        if (_playerData.Lvl % 10 == 0)
        {
            _playerData.MaxHp += 10;
            _playerData.Hp = _playerData.MaxHp;
            _playerData.MaxMana += 10;
            _playerData.Mana = _playerData.MaxMana;
        }

        _playerData.PhysicsDamage++;
        _playerData.MagicDamage++;
        _playerData.MoveSpeed *= 0.01f;
    }

    private void UpgradeCompetence()
    {
        _playerData.CompetenceTree.CompetenceLvlUp(_playerData.Competences, _playerData.Lvl);
    }

    #endregion
}
