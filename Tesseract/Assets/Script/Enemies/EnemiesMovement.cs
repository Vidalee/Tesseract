using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private BoxCollider2D bc2D;
    private GameObject player;

    
    public int Speed;
    public LayerMask blockingLayer;



    private void Start()
    {
        player = GameObject.Find("Player");
        rb2D = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
    }

 
    private void FixedUpdate()
    {
        Vector2 displacement = (player.transform.position - transform.position).normalized * Time.deltaTime * Speed;
        
        transform.Translate(displacement);
    }
}
