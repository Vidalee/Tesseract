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
    
    [SerializeField] protected int _physicsDamage;
    [SerializeField] protected float _MaxCooldown;
    [SerializeField] protected float DamageBoost;

    [SerializeField] protected int _ArmorP;
    [SerializeField] protected int _ArmorM;
    
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

    public void Create(EnemyData enemy, float x, float y, int lvl)
    {
        Lvl = lvl;
        name = enemy.name;
        _MaxHp = enemy.MaxHp;
        _Hp = enemy.Hp + 10 * lvl;
        _XpValue = enemy.XpValue * 1.2f;
        _physicsDamage = enemy.PhysicsDamage + (int) (lvl * enemy.DamageBoost);
        _ArmorM = enemy._ArmorM + lvl / 5;
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
        effectY = enemy.EffectY;
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

    public int PhysicsDamage => _physicsDamage;

    public int ArmorP => _ArmorP;

    public int ArmorM => _ArmorM;

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
}
