
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int Speed;
    public LayerMask blockingLayer;
    public float feetWidth;
    public float feetHeight;
 
    private void FixedUpdate()
    {
        PlayerDisplacement();
    }

    private void PlayerDisplacement()
    {
        //Get and set 
        int xDir = (int) Input.GetAxisRaw("Horizontal");
        int yDir = (int) Input.GetAxisRaw("Vertical");
        
        Vector3 xVec = new Vector3(xDir,0,0);
        Vector3 yVec = new Vector3(0,yDir,0);
                
        RaycastHit2D xLinecast = Physics2D.Linecast(transform.position, transform.position + xVec, blockingLayer);
        RaycastHit2D yLinecast = Physics2D.Linecast(transform.position, transform.position + yVec, blockingLayer);

        Vector3 direction = new Vector3(xDir,yDir,0);

        if (xLinecast) direction.x *= xLinecast.distance - feetWidth;
        if (yLinecast) direction.y *= yLinecast.distance - feetHeight;
        
        transform.Translate(direction * Speed * Time.deltaTime);
    }
}
