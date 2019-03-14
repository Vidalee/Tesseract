
using System;
using System.Collections;
using System.Globalization;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int Speed;
    public LayerMask blockingLayer;
    public float width;
    public float height;

    private Animator animator;
    
    private void FixedUpdate()
    {
        PlayerDisplacement();
    }

    private void Awake()
    {
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

    public void Shuriken1Animation(Vector3 cursorPos)
    {
        if (animator.GetBool("OtherAction")) return;
        StartCoroutine(shuriken1A(cursorPos));
    }
    
    IEnumerator shuriken1A(Vector3 cursorPos)
    {       
        animator.SetBool("OtherAction", true);
        Vector3 diff = cursorPos - transform.position;
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

        yield return new WaitForSeconds(0.2f);
        animator.SetBool("OtherAction", false);
    }
    
    private void PlayerDisplacement()
    {
        //Get and set 
        int xDir = (int) Input.GetAxisRaw("Horizontal");
        int yDir = (int) Input.GetAxisRaw("Vertical");
        
        PlayerMovingAnimation(xDir, yDir);
        
        Vector3 xVec = new Vector3(xDir,0,0);
        Vector3 yVec = new Vector3(0,yDir,0);

        Vector3 playerPos = transform.position;
        playerPos.y += -0.5f + height;
        
        RaycastHit2D xLinecast = Physics2D.Linecast(playerPos, playerPos + xVec, blockingLayer);
        RaycastHit2D yLinecast = Physics2D.Linecast(playerPos, playerPos + yVec, blockingLayer);

        Vector3 direction = new Vector3(xDir,yDir,0);

        if (xLinecast)
        {
            direction.x *= xLinecast.distance - width - 0.01f;
        }

        if (yLinecast)
        {
            direction.y *= yLinecast.distance - height - 0.01f;
        }
        
        transform.Translate(direction * Speed * Time.deltaTime);
    }
}
