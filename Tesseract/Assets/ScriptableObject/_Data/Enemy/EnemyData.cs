using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Script.Pathfinding;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[CreateAssetMenu(fileName = "Enemies", menuName = "Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] protected int _MaxHp;
    [SerializeField] private int _Hp;
    private int _lvl;
    [SerializeField] protected int _XpValue;

    
    [SerializeField] protected int _PhysicsDamage;
    [SerializeField] protected int _MagicDamage;
    [SerializeField] protected int _MaxCooldown;
    
    [SerializeField] protected float _MoveSpeed;
    [SerializeField] protected int _AttackRange;
    [SerializeField] protected int _DetectionRange;
    
    [SerializeField] protected Vector3 _StartPos;

    [SerializeField] public List<Node> Path;

    [SerializeField] protected CompetencesData[] _Competences;

    [SerializeField] protected Sprite _Sprite;

    [SerializeField] protected bool _Triggered;
    public bool OnHisWayBack;


    public void Create(EnemyData enemy, int x, int y)
    { 
        _MaxHp = enemy.MaxHp;
        _Hp = enemy.Hp;
        _XpValue = enemy.XpValue;
        _PhysicsDamage = enemy.PhysicsDamage;
        _MagicDamage = enemy.MagicDamage;
        _MaxCooldown = enemy.MaxCooldown;
        _MoveSpeed = enemy.MoveSpeed;
        _AttackRange = enemy.AttackRange;
        _DetectionRange = enemy._DetectionRange;
        _StartPos = new Vector3(x, y);
        _Competences = enemy.Competences;
        _Sprite = enemy.Sprite;
        Path = new List<Node>();
        _Triggered = false;
        OnHisWayBack = false;
    }

    public int MaxHp => _MaxHp;

    public int Hp
    {
        get => _Hp;
        set => _Hp = value;
    }

    public int Lvl
    {
        get => _lvl;
        set => _lvl = value;
    }

    public int XpValue => _XpValue;

    public int PhysicsDamage => _PhysicsDamage;

    public int MagicDamage => _MagicDamage;

    public float MoveSpeed
    {
        get => _MoveSpeed;
        set => _MoveSpeed = value;
    }

    public int AttackRange => _AttackRange;

    public int DetectionRange => _DetectionRange;

    public int MaxCooldown => _MaxCooldown;

    public CompetencesData[] Competences => _Competences;
    
    public CompetencesData GetCompetence(string name)
    {
        foreach (var c in _Competences)
        {
            if (c.Name == name) return c;
        }
        throw new Exception("Competence not found");
    }

    public Sprite Sprite => _Sprite;
    
    public bool Triggered
    {
        get => _Triggered;
        set => _Triggered = value;
    }

    public Vector3 StartPos => _StartPos;

    public void UpdateStats(int lvl, int floor)
    {
        _MaxHp += _MaxHp / 10 * lvl + (floor - 1) * _MaxHp;
        _Hp += _Hp / 10 * lvl + (floor - 1) * _Hp;
        //_XpValue += _XpValue / 10 * lvl + (floor - 1) * _XpValue;
        _PhysicsDamage += (int)(((float)_PhysicsDamage) / 10 * lvl) + (floor - 1) * _PhysicsDamage;
        _MagicDamage += _MagicDamage / 10 * lvl + (floor - 1) * _MagicDamage;
    }

    public void SetStats()
    {
        _MaxHp += 10 * _lvl;
        _Hp = _MaxHp;
        _XpValue += 10 * _lvl;
        _PhysicsDamage += _lvl;
        _MagicDamage += _lvl;
    }
}
