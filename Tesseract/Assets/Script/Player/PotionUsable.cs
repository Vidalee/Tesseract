using System.Collections;
using UnityEngine;

public class PotionUsable : MonoBehaviour
{
    #region Variable

    public PlayerData _playerData;
    private bool _usable = true;

    #endregion

    #region Initialise

    public void Create(PlayerData playerData)
    {
        _playerData = playerData;
        playerData.Inventory.SetAth();
    }

    #endregion

    #region Update

    void Update()
    {
        if (_usable)
        {
            if (Input.GetKey("p")) CallCoroutine(3);
            if (Input.GetKey("o")) CallCoroutine(2);
            if (Input.GetKey("i")) CallCoroutine(1);
            if (Input.GetKey("u")) CallCoroutine(0);
        }
    }

    #endregion

    #region Potion

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
                AddHp(potion.HpHeal);
                break;
            case "mana":
                AddMana(potion.ManaHeal);
                break;
            case"livemana":
                AddHp(potion.HpHeal);
                AddMana(potion.ManaHeal);
                break;
        }
    }

    #endregion

    #region Effects

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

    #endregion

    #region Utilities

    private void CallCoroutine(int index)
    {
        Potions pot = _playerData.Inventory.UsePotion(index);
        if (pot == null) return;
        StartCoroutine(UsePotion(pot));
    }

    #endregion
}
