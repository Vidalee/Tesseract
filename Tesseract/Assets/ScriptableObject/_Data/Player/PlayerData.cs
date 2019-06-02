using System;
using Script.GlobalsScript.Struct;
using Script.Pathfinding;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Data")]
public class PlayerData : ScriptableObject
{
    #region Variable

    public GameEvent PlayerHpAth;
    public GameEvent PlayerHpBarAth;
    public GameEvent PlayerManaAth;
    public GameEvent PlayerManaBarAth;
    public GameEvent PlayerXpAth;
    public GameEvent PlayerXpBarAth;
    public GameEvent PlayerLevelAth;

    [SerializeField] protected string _Name;
    [SerializeField] protected int _MultiID;

    [SerializeField] protected int _MaxHp;
    [SerializeField] private int _Hp;
    [SerializeField] protected int _MaxMana;
    [SerializeField] private int _Mana;

    [SerializeField] protected int _ManaRegen;

    [SerializeField] protected int _PhysicsDamage;
    [SerializeField] protected int _MagicDamage;
    [SerializeField] protected float _MoveSpeed;

    [SerializeField] protected long _MaxXp;

    public long _Xp;
    [SerializeField] protected int _MaxLvl;
    public int _Lvl;

    private bool _CanMove;

    [SerializeField] protected float _Width ;
    [SerializeField] protected float _Height;
    [SerializeField] protected float _FeetHeight;

    [SerializeField] protected AnimationClip[] _Move;
    [SerializeField] protected AnimationClip[] _Idle;
    [SerializeField] protected AnimationClip[] _Attack;
    [SerializeField] protected AnimationClip[] _Dash;
    [SerializeField] protected AnimationClip _DashParticles;
    [SerializeField] protected AnimationClip[] compAnim;

    [SerializeField] protected CompetencesData[] _Competences;
    [SerializeField] protected Inventory _Inventory;

    [SerializeField] protected float _PotionsCooldown;

    public int CompPoint;

    public bool PositionChanged;
    public Node Node;

    private int stateProj;
    private Color[] color;

    public void Create(PlayerData playerData, int[] lvl = null)
    {
        PlayerHpAth = playerData.PlayerHpAth;
        PlayerHpBarAth = playerData.PlayerHpBarAth;
        PlayerManaAth = playerData.PlayerManaAth;
        PlayerManaBarAth = playerData.PlayerManaBarAth;
        PlayerXpAth = playerData.PlayerXpAth;
        PlayerXpBarAth = playerData.PlayerXpBarAth;
        PlayerLevelAth = playerData.PlayerLevelAth;
        
        _Name = playerData.name;
        
        _Lvl = lvl == null ? 0 : lvl[0];
        
        _MaxHp = playerData.MaxHp + Lvl * 5;
        _Hp = _MaxHp;
        _MaxMana = playerData.MaxMana + Lvl * 5;
        _Mana = MaxMana;
        _ManaRegen = playerData.ManaRegen + Lvl;
        _PhysicsDamage = playerData.PhysicsDamage + Lvl * 5;
        _MagicDamage = playerData.MagicDamage + Lvl * 5;
        _MoveSpeed = playerData.MoveSpeed;
        _MaxXp = _Lvl == 0 ? playerData.MaxXp : (int) (Mathf.Pow(1.2f, Lvl) * playerData.MaxXp);
        _Xp = playerData.Xp;
        _MaxLvl = 99;
        
        _Width = 0.5f;
        _Height = 0.75f;
        _FeetHeight = 0.06f;
        
        _Move = playerData.Move;
        _Idle = playerData.Idle;
        _Attack = playerData.Attack;
        _Dash = playerData.Dash;
        _DashParticles = playerData.DashParticles;
        compAnim = playerData.compAnim;

        _Competences = new CompetencesData[4];

        _Competences[0] = CreateInstance<DashComp>();
        if (Name == "Warrior") _Competences[1] = CreateInstance<CacComp>();
            else _Competences[1] = CreateInstance<ProjComp>();
        _Competences[2] = CreateInstance<ProjComp>();
        _Competences[3] = CreateInstance<BoostComp>();

        for (int i = 0; i < 4; i++)
            _Competences[i].Create(playerData.Competences[i], lvl == null ? 0 : lvl[i + 1]);
        
        _Inventory = CreateInstance<Inventory>();
        _Inventory.Create(playerData._Inventory);
        _PotionsCooldown = playerData.PotionsCooldown;
        
        stateProj = 0;
        
        _CanMove = true;

        color = new[]
        {
            new Color(198, 198, 198), new Color(149, 210, 205), new Color(96, 121, 81), new Color(240, 225, 124), new Color(216, 34, 6),
        };
    }

    public AnimationClip AnimProj()
    {
        return compAnim[stateProj];
    }

    public Color AnimColor()
    {
        return color[stateProj];
    }

    public int Effect()
    {
        return stateProj;
    }

    public int Prob()
    {
        if (stateProj != 0) return Inventory.Weapon.EffectProb;
        return 0;
    }

    public int EffectDamage()
    {
        if (stateProj == 0 || Inventory.Weapon == null) return 0;
        return Inventory.Weapon.EffectDamage;
    }

    public float Duration()
    {
        if (stateProj == 0 || Inventory.Weapon == null) return 0;
        return Inventory.Weapon.Duration;
    }

    public void ReduceCd(float change)
    {
        foreach (var comp in Competences)
        {
            comp.Cooldown *= change;
        }
    }
    #endregion

    #region Set/Get
    public int StateProj
    {
        get => stateProj;
        set => stateProj = value;
    }

    public int MultiID
    {
        get => _MultiID;
        set => _MultiID = value;
    }

    public int ManaRegen
    {
        get => _ManaRegen;
        set => _ManaRegen = value;
    }

    public int MaxHp
    {
        get => _MaxHp;
        set => _MaxHp = value;
    }

    public int MaxMana
    {
        get => _MaxMana;
        set => _MaxMana = value;
    }

    public bool CanMove
    {
        get => _CanMove;
        set => _CanMove = value;
    }

    public int Hp
    {
        get => _Hp;
        set
        {
            _Hp = value;

            PlayerHpAth.Raise(new EventArgsString(_Hp + ""));
            PlayerHpBarAth.Raise(new EventArgsFloat((float) Hp/MaxHp));
        }
    }

    public int Mana
    {
        get => _Mana;
        set
        {
            _Mana = value;

            PlayerManaAth.Raise(new EventArgsString(_Mana + ""));
            PlayerManaBarAth.Raise(new EventArgsFloat((float) _Mana/_MaxMana));
        }
    }

    public long Xp
    {
        get => _Xp;
        set
        {
            _Xp = value;

            PlayerXpAth.Raise(new EventArgsString(_Xp + ""));
            PlayerXpBarAth.Raise(new EventArgsFloat((float) _Xp/_MaxXp));
        }
    }

    public int Lvl
    {
        get => _Lvl;
        set
        {
            _Lvl = value;

            PlayerLevelAth.Raise(new EventArgsString(_Lvl + ""));
        }
    }

    public float PotionsCooldown => _PotionsCooldown;

    public int PhysicsDamage
    {
        get => _PhysicsDamage;
        set => _PhysicsDamage = value;
    }

    public int MagicDamage
    {
        get => _MagicDamage;
        set => _MagicDamage = value;
    }

    public float MoveSpeed
    {
        get => _MoveSpeed;
        set => _MoveSpeed = value;
    }

    public long MaxXp
    {
        get => _MaxXp;
        set => _MaxXp = value;
    }

    public int MaxLvl => _MaxLvl;

    public string Name => _Name;

    public float Width => _Width;

    public float Height => _Height;

    public float FeetHeight => _FeetHeight;

    public CompetencesData[] Competences
    {
        get => _Competences;
        set => _Competences = value;
    }

    public AnimationClip[] Move => _Move;

    public AnimationClip[] Idle => _Idle;

    public AnimationClip[] Attack => _Attack;

    public Inventory Inventory => _Inventory;

    public AnimationClip[] Dash => _Dash;

    public AnimationClip DashParticles => _DashParticles;

    #endregion
}
