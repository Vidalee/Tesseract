using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Live : MonoBehaviour
{

    public int live;

    public void Damage(int damage)
    {
        live -= damage;
        Death();
    }

    public void Death()
    {
        if (live <= 0)
        {
            Destroy(gameObject);
            GameObject map = GameObject.Find("Map");
            Destroy(map);
        }
    }
}
