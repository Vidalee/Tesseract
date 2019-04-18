using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Items/GameItems/Potions")]
public class Potions : GamesItem
{
    [SerializeField] protected string type;
    [SerializeField] protected int heal;

    public string Type => type;

    public int Heal => heal;
}
