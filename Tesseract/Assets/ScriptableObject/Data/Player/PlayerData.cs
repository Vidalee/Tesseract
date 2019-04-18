using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField] protected int _MaxHp;
    [SerializeField] private int _Hp;
    [SerializeField] protected int _MaxMana;
    [SerializeField] private int _Mana;
    [SerializeField] protected int _PhysicsDamage;
    [SerializeField] protected int _MagicDamage;
    
    [SerializeField] protected float _MoveSpeed;
    [SerializeField] protected float _DashDistance;

    [SerializeField] protected float _Width ;
    [SerializeField] protected float _Height;
    [SerializeField] protected float _FeetHeight;

    [SerializeField] protected AnimationClip[] _Move;
    [SerializeField] protected AnimationClip[] _Idle;
    [SerializeField] protected AnimationClip[] _Throw;
    [SerializeField] protected AnimationClip[] _Dash;
    
    [SerializeField] protected CompetencesData[] _Competences;
    [SerializeField] protected Inventory _Inventory;

    [SerializeField]protected float _PotionsCooldown;
    
    
    private void OnEnable()
    {
        _Hp = _MaxHp;
        _Mana = _MaxMana;
    }

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

    public CompetencesData GetCompetence(string name)
    {
        foreach (var c in _Competences)
        {
            if (c.Name == name) return c;
        }
        throw new Exception("Competence not found");
    }
    
    public float Width => _Width;

    public float Height => _Height;
    
    public float FeetHeight => _FeetHeight;
    
    public CompetencesData[] Competences => _Competences;
    
    public AnimationClip[] Move => _Move;

    public AnimationClip[] Idle => _Idle;

    public AnimationClip[] Throw => _Throw;

    public Inventory Inventory => _Inventory;

    public AnimationClip[] Dash => _Dash;

}