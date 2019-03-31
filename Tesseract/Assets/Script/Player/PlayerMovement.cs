
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
        playerPos.x += xDir * PlayerData.Width / 2;
        
        RaycastHit2D xLinecast = Physics2D.Linecast(playerPos, playerPos + new Vector3(xDir, 0, 0), BlockingLayer);
        RaycastHit2D yLinecast = Physics2D.Linecast(playerPos, playerPos + new Vector3(0, yDir, 0), BlockingLayer);

        Vector3 direction = new Vector3(xDir,yDir,0);

        if (xLinecast)
        {
            direction.x *= xLinecast.distance;
        }

        if (yLinecast)
        {
            direction.y *= yLinecast.distance;
        }
        
        transform.Translate(direction * PlayerData.MoveSpeed * Time.deltaTime);
    }
}
