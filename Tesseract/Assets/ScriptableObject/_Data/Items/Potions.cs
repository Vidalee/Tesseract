using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Items/GameItems/Potions")]
public class Potions : GamesItem
{
    [SerializeField] protected string type;
    [SerializeField] protected int hpHeal;
    [SerializeField] protected int manaHeal;

    public string Type => type;

    public int HpHeal => hpHeal;

    public int ManaHeal => manaHeal;
}
