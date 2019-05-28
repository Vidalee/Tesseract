using UnityEngine;
[CreateAssetMenu(fileName = "Weapons", menuName = "Items/GameItems/Weapons")]

public class Weapons : GamesItem
{
    [SerializeField] protected int physicsDamage;
    [SerializeField] protected Vector2[] colliderPoints;

    public Vector2[] ColliderPoints => colliderPoints;

    public void Create(Weapons weapon)
    {
        icon = weapon.icon;
        displayName = weapon.displayName;
        description = weapon.description;
        physicsDamage = weapon.physicsDamage;
        colliderPoints = weapon.colliderPoints;
    }
}
