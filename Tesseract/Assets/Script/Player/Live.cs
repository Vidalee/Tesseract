using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Live : MonoBehaviour
{

    [SerializeField] protected PlayerData PlayerData;

    public void Damage(int damage)
    {
        PlayerData.Hp -= damage;
        Death();
    }

    public void Death()
    {
        if (PlayerData.Hp <= 0)
        {
            Destroy(gameObject);
            GameObject map = GameObject.Find("Map");
            Destroy(map);
        }
    }
}
