using UnityEngine;

public abstract class CompetencesData : ScriptableObject
{
    [SerializeField] protected string name;
    [SerializeField] protected float cooldown;
    [SerializeField] protected int manaCost;
    [SerializeField] protected string id;
    private bool usable;
    private int lvl;
    
    [SerializeField] protected string allyTag;
    [SerializeField] protected string enemyTag;

    public float Cooldown
    {
        get => cooldown;
        set => cooldown = value;
    }

    public bool Usable
    {
        get => usable;
        set => usable = value;
    }

    public int ManaCost
    {
        get => manaCost;
        set => manaCost = value;
    }
    
    public void Create(CompetencesData comp, int lvl)
    {
        name = comp.name;
        cooldown = comp.cooldown;
        manaCost = comp.manaCost;
        usable = true;
        allyTag = comp.allyTag;
        enemyTag = comp.enemyTag;
        ChildCreate(comp, lvl);
    }

    public abstract void ChildCreate(CompetencesData comp, int lvl);

    public string Name => name;

    public string AllyTag => allyTag;

    public string EnemyTag => enemyTag;

    public string Id => id;

    public int Lvl => lvl;
}
