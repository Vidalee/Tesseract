using System;
using UnityEditor.Experimental.U2D;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private BoxCollider2D bc2D;

    
    public int Speed;
    public LayerMask blockingLayer;



    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
    }

 
    private void FixedUpdate()
    {
        int xDir = (int) Input.GetAxisRaw("Horizontal");
        int yDir = (int) Input.GetAxisRaw("Vertical");

        RaycastHit hit;
        
        
        Vector2 displacement = new Vector2(xDir,yDir) * Time.deltaTime * Speed;

        // if (Physics2D.Raycast(transform.position, displacement, out hit, blockingLayer)) return;
            
        transform.Translate(displacement);
    }
}
