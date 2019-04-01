
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected PlayerData PlayerData;
    [SerializeField] protected LayerMask BlockingLayer;
    [SerializeField] protected GameEvent PlayerMoveEvent;
        
    private void FixedUpdate()
    {
        PlayerMove();
    }
    
    private void PlayerMove()
    {
        int xDir = (int) Input.GetAxisRaw("Horizontal");
        int yDir = (int) Input.GetAxisRaw("Vertical");
        
        PlayerMoveEvent.Raise(new EventArgsCoor(xDir, yDir));

        if (xDir == 0 && yDir == 0) return;
        
        Vector3 playerPos = transform.position;
        playerPos.y += -PlayerData.Height / 2;
        if (yDir > 0) playerPos.y += PlayerData.FeetHeight;
        Vector3 playerWidth = new Vector3(PlayerData.Width / 2, 0, 0);
        
        RaycastHit2D xLinecast = Physics2D.Linecast(playerPos + xDir * playerWidth, playerPos + new Vector3(xDir, 0, 0), BlockingLayer);
        RaycastHit2D yLeftLinecast = Physics2D.Linecast(playerPos + playerWidth, playerPos + new Vector3(0, yDir, 0), BlockingLayer);
        RaycastHit2D yRightLinecast = Physics2D.Linecast(playerPos - playerWidth, playerPos + new Vector3(0, yDir, 0), BlockingLayer);

        Vector3 direction = new Vector3(xDir,yDir,0);
        
        RaycastHit2D diagLinecast = Physics2D.Linecast(playerPos, playerPos + direction, BlockingLayer);
        if (diagLinecast) direction *= diagLinecast.distance;
     
        if (xLinecast)
        {
            direction.x *= xLinecast.distance;
        }

        if (yLeftLinecast)
        {
            direction.y *= yLeftLinecast.distance;
        }
        
        if (!yLeftLinecast && yRightLinecast)
        {
            direction.y *= yRightLinecast.distance;
        }
        
        transform.Translate(direction * PlayerData.MoveSpeed * Time.deltaTime);
    }
}
