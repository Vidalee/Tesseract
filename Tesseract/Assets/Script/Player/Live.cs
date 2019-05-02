using System.Collections;
using UnityEngine;

public class Live : MonoBehaviour
{
    #region Variable

    public PlayerData _playerData;
    public GameEvent PlayerLive;

    #endregion

    #region Initialise

    public void Create(PlayerData playerData)
    {
        _playerData = playerData;

        StartCoroutine(ManaRegen());
    }

    #endregion

    #region Damage

    public void Damage(IEventArgs args)
    {
        int damage = ((EventArgsInt) args).X;
        
        _playerData.Hp -= damage;
        PlayerLive.Raise(new EventArgsInt(_playerData.Hp));
        Death();
    }
    
    public void Damage(int x)
    {
        //Anti Merge conflict rofl
    }
    
    private void Death()
    {
        if (_playerData.Hp <= 0)
        {
            Destroy(gameObject);
            GameObject map = GameObject.Find("Map");
            Destroy(map);
        }
    }

    private IEnumerator ManaRegen()
    {
        while (_playerData.Hp > 0)
        {
            if (_playerData.Mana + _playerData.ManaRegen > _playerData.MaxMana) _playerData.Mana = _playerData.MaxMana;
            else _playerData.Mana += _playerData.ManaRegen;
            
            yield return new WaitForSeconds(1);
        }
    }

    #endregion
}
