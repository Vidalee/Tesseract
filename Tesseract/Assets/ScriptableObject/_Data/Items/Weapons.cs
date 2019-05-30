using UnityEngine;
[CreateAssetMenu(fileName = "Weapons", menuName = "Items/GameItems/Weapons")]

public class Weapons : GamesItem
{
    [SerializeField] protected int physicDamage;
    [SerializeField] protected int magicDamage;
    [SerializeField] protected Vector2[] colliderPoints;
    [SerializeField] public bool inPlayerInventory = false;
    public string _class;

    public Vector2[] ColliderPoints => colliderPoints;
    public int PhysicDamage => physicDamage;
    public int MagicDamage => magicDamage;

    public void Create(Weapons weapon, int lvl)
    {
        icon = weapon.icon;
        displayName = weapon.displayName;
        description = weapon.description;
        physicDamage = weapon.physicDamage + (int)((float) weapon.physicDamage / 10 * lvl);
        magicDamage = weapon.magicDamage + (int)((float) weapon.magicDamage / 10 * lvl);
        colliderPoints = weapon.colliderPoints;
        _class = weapon._class;
    }
}
