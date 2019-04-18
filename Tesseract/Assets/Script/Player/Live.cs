using UnityEngine;

public class Live : MonoBehaviour
{

    public PlayerData _playerData;

    public void Create(PlayerData playerData)
    {
        _playerData = playerData;
    }
    
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
}
