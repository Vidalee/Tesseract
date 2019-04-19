using UnityEngine;

public class Live : MonoBehaviour
{
    #region Variable

    public PlayerData _playerData;


    #endregion

    #region Initialise

    public void Create(PlayerData playerData)
    {
        _playerData = playerData;
    }

    #endregion

    #region Damage

    public void Damage(int damage)
    {
        _playerData.Hp -= damage;
        Death();
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
