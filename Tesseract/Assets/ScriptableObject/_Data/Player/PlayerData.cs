using System;
using Script.GlobalsScript.Struct;
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

    [SerializeField] protected int _MaxXp;

    public int _Xp;
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

    public bool PositionChanged;
    public Script.Pathfinding.Node Node;

    private int stateProj;
    private Color[] color;

    public AnimationClip AnimProj()
    {
        Debug.Log(stateProj);
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

    private void OnEnable()
    {
        _MaxLvl = 100;
        _Xp = 0;
        _Lvl = 1;
        _Hp = _MaxHp;
        _Mana = _MaxMana;
        _CanMove = true;

        color = new[]
        {
            new Color(198, 198, 198), new Color(149, 210, 205), new Color(96, 121, 81), new Color(240, 225, 124), new Color(216, 34, 6),
        };
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

    public int Xp
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

    public int MaxXp
    {
        get => _MaxXp;
        set => _MaxXp = value;
    }

    public int MaxLvl => _MaxLvl;

    public CompetencesData GetCompetence(string name)
    {
        foreach (var c in _Competences)
        {
            if (c.Name == name) return c;
        }
        throw new Exception("Competence not found");
    }

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
