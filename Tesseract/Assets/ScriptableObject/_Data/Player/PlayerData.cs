using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Data")]
public class PlayerData : ScriptableObject
{
    #region Variable

    [SerializeField] protected string _Name;
    
    [SerializeField] protected int _MaxHp;
    [SerializeField] private int _Hp;
    [SerializeField] protected int _MaxMana;
    [SerializeField] private int _Mana;
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
    
    [SerializeField] protected CompetencesData[] _Competences;
    [SerializeField] protected Inventory _Inventory;

    [SerializeField] protected float _PotionsCooldown;

    #endregion

    #region Initialise

    private void OnEnable()
    {
        _MaxLvl = 100;
        _Xp = 0;
        _Lvl = 1;
        _Hp = _MaxHp;
        _Mana = _MaxMana;
        _CanMove = true;
    }

    #endregion

    #region Set/Get

    public int MaxHp
    {
        get => _MaxHp;
        set => _MaxHp = value;
    }
    
    public int Hp
    {
        get => _Hp;
        set => _Hp = value;
    }

    public int MaxMana
    {
        get => _MaxMana;
        set => _MaxMana = value;
    }

    public int Mana
    {
        get => _Mana;
        set => _Mana = value;
    }

    public bool CanMove
    {
        get => _CanMove;
        set => _CanMove = value;
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

    public int Xp
    {
        get => _Xp;
        set => _Xp = value;
    }

    public int MaxXp
    {
        get => _MaxXp;
        set => _MaxXp = value;
    }

    public int Lvl
    {
        get => _Lvl;
        set => _Lvl = value;
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
    
    public CompetencesData[] Competences => _Competences;
    
    public AnimationClip[] Move => _Move;

    public AnimationClip[] Idle => _Idle;

    public AnimationClip[] Attack => _Attack;

    public Inventory Inventory => _Inventory;

    public AnimationClip[] Dash => _Dash;

    public AnimationClip DashParticles => _DashParticles;

    #endregion
}