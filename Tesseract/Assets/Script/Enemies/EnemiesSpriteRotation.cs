using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpriteRotation : MonoBehaviour
{
    private GameObject player;
    
    void Start()
    {
        player = GameObject.Find("Player");
    }
        
    void Update()
    {        
        Vector2 direction = player.transform.position - transform.position;
        transform.up = direction;
    }
}
