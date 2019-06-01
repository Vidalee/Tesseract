using System.Collections;
using System.Collections.Generic;
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
    public List<GamesItem> Items;

    public string Perso;

    public Transform Player;
    private PlayerData _playerData;

    private MapData _mapData;
    private TileData _miniMap;

    public GameObject armory;

    #endregion

    public PlayerData PlayerData => _playerData;

    #region Update

    private void Update()
    {
        if(Input.GetKey("x")) GetXp(10);
    }

    #endregion

    #region Initialise

    public void Create(int x, int y, int id, bool solo)
    {
        if (solo)
        {
            if (StaticData.ActualFloor == 0)
            {
                StaticData.ActualFloor = 1;
                string type = Perso != "" ? Perso : StaticData.PlayerChoice;
                int pers = FindClass(type);

                _playerData = _PlayersData[pers];
                _playerData.MultiID = id;

                LoadPlayer();

                InstantiatePlayer(x, y);
            }
            else
            {
                _playerData = StaticData.actualData;
                InstantiatePlayer(x, y);
            }
        }
        else
        {
            int pers = id - 1;

            _playerData = _PlayersData[pers];
            _playerData.MultiID = id;
            LoadPlayer();
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

        StartCoroutine(SetAth());

        AllNodes.players.Add(o);
        GenerateEnemies.players.Add(o);

    }

    IEnumerator SetAth()
    {
        yield return new WaitForSeconds(0.1f);
        _playerData.Inventory.SetAth();
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

    private void LoadPlayer()
    {
        PlayerDataSave data = SaveSystem.LoadPlayer(_playerData.Name);
        if (data == null)
        {
            ResetStats(FindClass(_playerData.Name));

            GamesItem ite = FindItems(0);
            Weapons i = ScriptableObject.CreateInstance<Weapons>();
            i.Create(ite as Weapons, 1);

            _playerData.Inventory.Weapon = i;

            _playerData.Inventory.Potions = new Potions[4];

            return;
        }

        LoadStats(data);

        GamesItem item = FindItems(data.weapon);
        Weapons it = ScriptableObject.CreateInstance<Weapons>();
        it.Create(item as Weapons, data.weaponLvl);

        _playerData.Inventory.AddItem(it, Vector3.zero);

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
        //Todo COMP SAVE
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
        
        //Todo COMP LOAD
    }

    private GamesItem FindItems(int id)
    {
        if (id == 0)
        {
            if (_playerData.name == "Warrior") id = 31;
            if (_playerData.name == "Mage") id = 21;
            if (_playerData.name == "Assassin") id = 11;
            if (_playerData.name == "Archer") id = 1;
        }
        foreach (var it in Items)
        {
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

    #endregion

    public void AddItem(IEventArgs item)
    {
        StartCoroutine(Wait(item));
    }

    IEnumerator Wait(IEventArgs item)
    {
        yield return new WaitForSeconds(0.2f);
        EventArgsItem itemArg = item as EventArgsItem;
        bool added = _playerData.Inventory.AddItem(itemArg.Item, transform.GetChild(0).position);
        if (itemArg.Item is Weapons w)
        {
            _playerData.StateProj = w.EffectType;
        }

        if(added) Destroy(itemArg.T.gameObject);
    }

    public void AddWeapons(IEventArgs args)
    {
        StartCoroutine(FuckIt(args));
    }

    IEnumerator FuckIt(IEventArgs args)
    {
        if ((args as EventArgsWeaponsAth).Weapons != null)
        {
            if (transform.childCount == 0)
            {
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                armory.GetComponent<ArmoryManager>().CreateWeapon((args as EventArgsWeaponsAth).Weapons, transform.GetChild(0), 1, transform.GetChild(0));
            }
        }
    }

    public void RemoveWeapon(IEventArgs args)
    {
        _playerData.Inventory.RemoveWeapon(transform.GetChild(0).position);
        _playerData.StateProj = 0;
    }

    public void RemovePotion(IEventArgs args)
    {
        _playerData.Inventory.RemovePotion((args as EventArgsInt).X, transform.GetChild(0).position);
    }
}
