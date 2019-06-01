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
    [SerializeField] protected new string name;
    
    [SerializeField] protected float _MaxHp;
    [SerializeField] private float _Hp;
    private int _lvl;
    [SerializeField] protected float _XpValue;

    
    [SerializeField] protected float _PhysicsDamage;
    [SerializeField] protected float _MagicDamage;
    [SerializeField] protected float _MaxCooldown;
    
    [SerializeField] protected float _MoveSpeed;
    [SerializeField] protected float _AttackRange;
    [SerializeField] protected float _DetectionRange;
    
    [SerializeField] protected Vector3 _StartPos;

    [SerializeField] public List<Node> Path;

    [SerializeField] protected CompetencesData[] _Competences;

    [SerializeField] protected Sprite _Sprite;

    [SerializeField] protected bool _Triggered;
    public bool OnHisWayBack;
    
    [SerializeField] protected AnimationClip[] move;
    [SerializeField] protected AnimationClip[] idle;
    [SerializeField] protected AnimationClip[] attack;

    [SerializeField] protected float colliderX;
    [SerializeField] protected float colliderY;

    [SerializeField] protected float effectY;

    [SerializeField] protected Vector3 feetPos;
    public void Create(EnemyData enemy, float x, float y)
    {
        name = enemy.name;
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
        move = enemy.move;
        idle = enemy.idle;
        attack = enemy.attack;
        colliderX = enemy.colliderX;
        colliderY = enemy.colliderY;
        feetPos = enemy.feetPos;
    }

    public string Name => name;
    public float MaxHp => _MaxHp;

    public float Hp
    {
        get => _Hp;
        set => _Hp = value;
    }

    public int Lvl
    {
        get => _lvl;
        set => _lvl = value;
    }

    public float XpValue => _XpValue;

    public float PhysicsDamage => _PhysicsDamage;

    public float MagicDamage => _MagicDamage;

    public float MoveSpeed
    {
        get => _MoveSpeed;
        set => _MoveSpeed = value;
    }

    public float AttackRange => _AttackRange;

    public float DetectionRange => _DetectionRange;

    public float MaxCooldown => _MaxCooldown;

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
    
    public AnimationClip[] Move => move;

    public AnimationClip[] Idle => idle;

    public AnimationClip[] Attack => attack;

    public float ColliderX => colliderX;

    public float ColliderY => colliderY;

    public float EffectY => effectY;

    public Vector3 FeetPos => feetPos;

    public void SetStats()
    {
        _MaxHp += 10 * _lvl;
        _Hp = _MaxHp;
        _XpValue += _XpValue / 4 * _lvl;
        _PhysicsDamage += _lvl;
        _MagicDamage += _lvl;
    }
}
