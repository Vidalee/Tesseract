using System;

[Serializable]
public class PlayerDataSave
{
    public int xp;
    public int weapon;
    public int[] inventory;
    
    public PlayerDataSave(PlayerData player)
    {
        xp = player.TotalXp;

        weapon = ItemNull(player.Inventory.Weapon);
        
        inventory = new[]
        {
            ItemNull(player.Inventory.Potions[0]),
            ItemNull(player.Inventory.Potions[1]),
            ItemNull(player.Inventory.Potions[2]),
            ItemNull(player.Inventory.Potions[3]),
        };
    }

    private int ItemNull(GamesItem item)
    {
        if (item == null) return 0;
        return item.id;
    }
}
