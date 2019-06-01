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

    
    [SerializeField] protected int _physicsDamage;
    [SerializeField] protected int _magicDamage;
    [SerializeField] protected int _MaxCooldown;

    [SerializeField] protected int _ArmorP;
    [SerializeField] protected int _ArmorM;
    
    [SerializeField] protected float _MoveSpeed;
    [SerializeField] protected int _AttackRange;
    [SerializeField] protected int _DetectionRange;
    
    [SerializeField] protected Vector3 _StartPos;

    [SerializeField] public List<Node> Path;

    [SerializeField] protected CompetencesData[] _Competences;

    [SerializeField] protected Sprite _Sprite;

    [SerializeField] protected bool _Triggered;
    public bool OnHisWayBack;


    public void Create(EnemyData enemy, int x, int y, int lvl)
    { 
        _MaxHp = enemy.MaxHp;
        _Hp = enemy.Hp;
        _XpValue = enemy.XpValue;
        _physicsDamage = enemy.PhysicsDamage;
        _magicDamage = enemy.MagicDamage;
        _ArmorM = enemy._ArmorM;
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

    public int PhysicsDamage => _physicsDamage;

    public int MagicDamage => _magicDamage;

    public int ArmorP => _ArmorP;

    public int ArmorM => _ArmorM;

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
}
