using UnityEngine;

public abstract class CompetencesData : ScriptableObject
{
    [SerializeField] protected string name;
    [SerializeField] protected float cooldown;
    [SerializeField] protected int manaCost;
    [SerializeField] protected string id;
    private bool usable;
    private int lvl;
    
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
        enemyTag = comp.enemyTag;
        id = comp.id;
        this.lvl = lvl;
        
        ChildCreate(comp, lvl);
    }

    public abstract void ChildCreate(CompetencesData comp, int lvl);

    public abstract void UpgradeStats();

    public string Name => name;

    public string EnemyTag => enemyTag;

    public string Id => id;

    public int Lvl
    {
        get => lvl;
        set => lvl = value;
    }
}
