using UnityEngine;
[CreateAssetMenu(fileName = "Weapons", menuName = "Items/GameItems/Weapons")]

public class Weapons : GamesItem
{
    [SerializeField] protected int magicDamage;

    [SerializeField] protected int physicsDamage;
    [SerializeField] protected float cd;
    [SerializeField] protected int effectDamage;
    [SerializeField] protected int effectProb;
    [SerializeField] protected int effectType;
    [SerializeField] protected Sprite effectSprite;
    [SerializeField] protected int lvl;
    
    [SerializeField] protected Vector2[] colliderPoints;
    [SerializeField] public bool inPlayerInventory = false;
    public string _class;

    public Vector2[] ColliderPoints => colliderPoints;

    public void Create(Weapons weapon, int lvl)
    {
        icon = weapon.icon;
        displayName = weapon.displayName;
        description = weapon.description;
        physicsDamage = weapon.physicsDamage + (int)((float) weapon.physicsDamage / 10 * lvl);
        magicDamage = weapon.magicDamage + (int)((float) weapon.magicDamage / 10 * lvl);
        colliderPoints = weapon.colliderPoints;
        _class = weapon._class;
        cd = weapon.cd;
        effectDamage = weapon.effectDamage;
        effectType = weapon.effectType;
        effectSprite = weapon.effectSprite;
    }

    public int PhysicsDamage => physicsDamage;

    public int MagicDamage => magicDamage;

    public float Cd => cd;

    public int EffectDamage => effectDamage;

    public int EffectType => effectType;

    public int EffectProb => effectProb;

    public Sprite EffectSprite => effectSprite;

    public int Lvl => lvl;
}
