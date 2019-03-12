using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesLive : MonoBehaviour
{
    public int live;
    
    public void GetDamaged(int damage)
    {
        live -= damage;
        if (live <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
