
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int Speed;
    public LayerMask blockingLayer;
    public Animator movementAnimator;

    private void FixedUpdate()
    {
        PlayerDisplacement();
    }

    private void PlayerMovingAnimation(int x, int y)
    {
        int speed = x != 0 ? x : y;
        bool dir = x == 0;
        
        if (speed == 0)
        {

            movementAnimator.SetInteger("Speed", 0);
            return;
        }
        
        movementAnimator.SetInteger("Speed",speed);
        movementAnimator.SetBool("Direction",dir);
        
    }

    private void PlayerProjectilesAnimation()
    {
        movementAnimator.SetBool("Projectiles", Input.GetMouseButtonDown(0));
    }
    
    private void PlayerDisplacement()
    {
        //Get and set 
        int xDir = (int) Input.GetAxisRaw("Horizontal");
        int yDir = (int) Input.GetAxisRaw("Vertical");
        
        PlayerMovingAnimation(xDir, yDir);
        PlayerProjectilesAnimation();
        
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
