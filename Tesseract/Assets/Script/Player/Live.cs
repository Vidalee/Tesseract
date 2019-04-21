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

    #endregion
}
