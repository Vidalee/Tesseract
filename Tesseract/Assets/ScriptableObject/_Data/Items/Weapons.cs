using UnityEngine;
[CreateAssetMenu(fileName = "Weapons", menuName = "Items/GameItems/Weapons")]

public class Weapons : GamesItem
{
    [SerializeField] private int physicsDamage { get; set; }
    [SerializeField] private int magicDamage { get; set; }
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
    }
}
