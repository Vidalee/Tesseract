using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionUsable : MonoBehaviour
{
    [SerializeField] protected PlayerData PlayerData;
    private bool _usable = true;
    
    void Update()
    {
        if (_usable)
        {
            if (Input.GetKey("p")) CallCoroutine(0);
            if (Input.GetKey("o")) CallCoroutine(1);
        }
    }

    private void CallCoroutine(int index)
    {
        Potions pot = PlayerData.Inventory.UsePotion(index);
        if (pot == null) return;
        StartCoroutine(UsePotion(pot));
    }
    
    IEnumerator UsePotion(Potions pot)
    {
        PotionEffect(pot);

        _usable = false;
        yield return new WaitForSeconds(PlayerData.PotionsCooldown);
        _usable = true;
    }

    private void PotionEffect(Potions potion)
    {
        switch (potion.Type)
        {
            case "live":
                AddHp(potion.Heal);
              break;
            case "mana":
                AddMana(potion.Heal);
                break;
        }
    }

    private void AddHp(int hp)
    {
        PlayerData.Hp += hp;
        if (PlayerData.Hp >= PlayerData.MaxHp) PlayerData.Hp = PlayerData.MaxHp;
    }

    private void AddMana(int mana)
    {
        PlayerData.Mana += mana;
        if (PlayerData.Mana >= PlayerData.MaxMana) PlayerData.Mana = PlayerData.MaxMana;
    }
}
