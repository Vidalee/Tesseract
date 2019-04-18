using System.Collections;
using UnityEngine;

public class PotionUsable : MonoBehaviour
{
    public PlayerData _playerData;
    private bool _usable = true;
    
    public void Create(PlayerData playerData)
    {
        _playerData = playerData;
    }
    
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
        Potions pot = _playerData.Inventory.UsePotion(index);
        if (pot == null) return;
        StartCoroutine(UsePotion(pot));
    }
    
    IEnumerator UsePotion(Potions pot)
    {
        PotionEffect(pot);

        _usable = false;
        yield return new WaitForSeconds(_playerData.PotionsCooldown);
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
        _playerData.Hp += hp;
        if (_playerData.Hp >= _playerData.MaxHp) _playerData.Hp = _playerData.MaxHp;
    }

    private void AddMana(int mana)
    {
        _playerData.Mana += mana;
        if (_playerData.Mana >= _playerData.MaxMana) _playerData.Mana = _playerData.MaxMana;
    }
}
