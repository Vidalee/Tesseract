using UnityEngine;

public class CompetencesData : ScriptableObject
{
    [SerializeField] protected string _Name;
    [SerializeField] protected float _Cooldown;
    [SerializeField] protected int manaCost;
    private bool _Usable;
    
    [SerializeField] protected Transform _Object;
    
    [SerializeField] protected string _AllyTag;
    [SerializeField] protected string _EnemyTag;

    public float Cooldown
    {
        get => _Cooldown;
        set => _Cooldown = value;
    }

    public bool Usable
    {
        get => _Usable;
        set => _Usable = value;
    }

    public int ManaCost
    {
        get => manaCost;
        set => manaCost = value;
    }

    public string Name => _Name;

    public Transform Object => _Object;

    public string AllyTag => _AllyTag;

    public string EnemyTag => _EnemyTag;
}
