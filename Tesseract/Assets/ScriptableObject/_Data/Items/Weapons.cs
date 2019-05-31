using UnityEngine;
[CreateAssetMenu(fileName = "Weapons", menuName = "Items/GameItems/Weapons")]

public class Weapons : GamesItem
{
    [SerializeField] protected int physicsDamage;
    [SerializeField] protected int magicDamage;
    [SerializeField] protected float cd;
    [SerializeField] protected int effectDamage;
    [SerializeField] protected string effetType;
    [SerializeField] protected Sprite effectSprite;
    [SerializeField] protected int lvl;
    [SerializeField] private AnimationClip ObjAnimation;
    
    [SerializeField] protected Vector2[] colliderPoints;

    public Vector2[] ColliderPoints => colliderPoints;

    public void Create(Weapons weapon, int lvl)
    {
        icon = weapon.icon;
        displayName = weapon.displayName;
        description = weapon.description;
        physicsDamage = weapon.physicsDamage + (int)((float) weapon.physicsDamage / 10 * lvl);
        magicDamage = weapon.magicDamage + (int)((float) weapon.magicDamage / 10 * lvl);
        colliderPoints = weapon.colliderPoints;
        cd = weapon.cd;
        effectDamage = weapon.effectDamage;
        effetType = weapon.effetType;
        effectSprite = weapon.effectSprite;
    }

    public int PhysicsDamage => physicsDamage;

    public int MagicDamage => magicDamage;

    public float Cd => cd;

    public int EffectDamage => effectDamage;

    public string EffetType => effetType;

    public Sprite EffectSprite => effectSprite;

    public int Lvl => lvl;
}
