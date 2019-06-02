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
    public GameEvent CompAth;

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

    #region Initialise

    public void Create(int x, int y, int id, bool solo)
    {
        if (solo)
        {
            if (StaticData.ActualFloor == 0)
            {
                StaticData.ActualFloor = 1;

                LoadPlayer();

                _playerData.MultiID = id;

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
            LoadPlayer(id);
            
            _playerData.MultiID = id;
            
            InstantiatePlayer(x, y);
        }
    }

    private void InstantiatePlayer(int x, int y)
    {
        StaticData.actualData = _playerData;
            
        Transform o = Instantiate(Player, new Vector3(x, y, 0), Quaternion.identity, transform);

        SetXp.Raise(new EventArgsString(_playerData.Xp.ToString()));
        SetXpBar.Raise(new EventArgsFloat((float) _playerData.Xp / _playerData.MaxXp));
        SetHp.Raise(new EventArgsString(_playerData.Hp.ToString()));
        SetHpBar.Raise(new EventArgsFloat((float) _playerData.Hp / _playerData.MaxHp));
        SetMana.Raise(new EventArgsString(_playerData.Mana.ToString()));
        SetManaBar.Raise(new EventArgsFloat((float) _playerData.Mana / _playerData.MaxMana));
        SetLvl.Raise(new EventArgsString(_playerData.Lvl.ToString()));
        CompAth.Raise(new EventArgsNull());
        
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
        }

        return index;
    }

    private void LoadPlayer(int pers = 0)
    {
        string type = Perso != "" ? Perso : StaticData.PlayerChoice;
        pers = pers == 0 ? FindClass(type) : pers - 1;
        
        PlayerDataSave data = SaveSystem.LoadPlayer(type);
        if (data == null)
        {
            ResetStats(pers);

            GamesItem ite = FindItems(0);
            Weapons i = ScriptableObject.CreateInstance<Weapons>();
            i.Create(ite as Weapons, 0);

            _playerData.Inventory.Weapon = i;

            return;
        }

        ResetStats(pers, data.Lvl);
        _playerData.Xp = data.Xp;

        GamesItem item = FindItems(data.weapon);
        Weapons it = ScriptableObject.CreateInstance<Weapons>();
        it.Create(item as Weapons, data.weaponLvl);
                
        _playerData.Inventory.AddItem(it, Vector3.zero);
        _playerData.StateProj = it.EffectType;

        _playerData.Inventory.Potions = new Potions[4];
    }

    private void ResetStats(int index, int[] lvl = null)
    {
        _playerData = ScriptableObject.CreateInstance<PlayerData>();
        _playerData.Create(_PlayersDataCopy[index], lvl);
    }

    private GamesItem FindItems(int id)
    {
        if (id == 0)
        {
            if (_playerData.Name == "Warrior") id = 31;
            if (_playerData.Name == "Mage") id = 21;
            if (_playerData.Name == "Assassin") id = 11;
            if (_playerData.Name == "Archer") id = 1;
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

    public void GetXp(long amout)
    {
        long gap = _playerData.MaxXp - _playerData.Xp;

        while (amout >= gap)
        {
            amout = amout - gap;

            if (_playerData.Lvl < _playerData.MaxLvl) _playerData.Lvl++;
            _playerData.Xp = 0;

            _playerData.MaxXp = (int) (_playerData.MaxXp * 1.2f);
            
            UpgradeStats();

            gap = _playerData.MaxXp;
        }

        _playerData.Xp += amout;
    }

    #endregion

    #region LvlUp

    private void UpgradeStats()
    {

        _playerData.MaxHp += 5;
        _playerData.Hp += 5;
        _playerData.MaxMana += 5;
        _playerData.Mana += 5;
        _playerData.ManaRegen++;
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
        if (transform.childCount == 0)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            if ((args as EventArgsWeaponsAth).Weapons != null)
            {
                armory.GetComponent<ArmoryManager>().CreateWeapon((args as EventArgsWeaponsAth).Weapons,
                    transform.GetChild(0), 1, transform.GetChild(0));
            }
            else
            {
                Destroy(transform.GetChild(0).GetChild(5).gameObject);
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
