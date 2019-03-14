
using System;
using System.Collections;
using System.Globalization;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int Speed;
    public LayerMask blockingLayer;
    
    private Animator animator;
    private NinjaAttack scriptAttack;
    
    private void FixedUpdate()
    {
        PlayerDisplacement();
    }

    private void Awake()
    {
        scriptAttack = GetComponent<NinjaAttack>();
        animator = GetComponent<Animator>();
    }

    private void PlayerMovingAnimation(int x, int y)
    {
        int speed = x != 0 ? x : y;
        bool dir = x == 0;
        
        if (speed == 0)
        {
            animator.SetInteger("Speed", 0);
            return;
        }
        
        animator.SetInteger("Speed",speed);
        animator.SetBool("Direction",dir);
        
    }



    public void Shuriken1Animation(Vector3 cameraPos)
    {
        Vector3 diff = cameraPos - transform.position;
        bool dir = Math.Abs(diff.x) > Math.Abs(diff.y);

        if (dir)
        {
            if (diff.x < 0)
            {
                animator.Play("NinjaProjectilesLeft");
            }
            else
            {
                animator.Play("NinjaProjectilesRight");
            }
        }
        else
        {
            if (diff.y < 0)
            {
                animator.Play("NinjaProjectilesBottom");
            }
            else
            {
                animator.Play("NinjaProjectilesTop");
            }
        }
        
    }
    
    private void PlayerDisplacement()
    {
        //Get and set 
        int xDir = (int) Input.GetAxisRaw("Horizontal");
        int yDir = (int) Input.GetAxisRaw("Vertical");
        
        PlayerMovingAnimation(xDir, yDir);
        
        Vector3 xVec = new Vector3(xDir,0,0);
        Vector3 yVec = new Vector3(0,yDir,0);
        
        RaycastHit2D xLinecast = Physics2D.Linecast(transform.position, transform.position + xVec, blockingLayer);
        RaycastHit2D yLinecast = Physics2D.Linecast(transform.position, transform.position + yVec, blockingLayer);

        Vector3 direction = new Vector3(xDir,yDir,0);

        if (xLinecast) direction.x *= xLinecast.distance;
        if (yLinecast) direction.y *= yLinecast.distance;
        
        transform.Translate(direction * Speed * Time.deltaTime);
    }
}
