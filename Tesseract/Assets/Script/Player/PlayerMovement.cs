
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected PlayerData PlayerData;
    [SerializeField] protected LayerMask BlockingLayer;
    [SerializeField] protected GameEvent PlayerMoveEvent;
        
    private void Update()
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
        Vector3 direction = new Vector3(xDir,yDir,0);

        RaycastHit2D xLinecast = Physics2D.Linecast(playerPos + xDir * playerWidth, playerPos + xDir * playerWidth * 2, BlockingLayer);
        RaycastHit2D yLeftLinecast = Physics2D.Linecast(playerPos + playerWidth, playerPos + new Vector3(0, yDir) + playerWidth, BlockingLayer);
        RaycastHit2D yRightLinecast = Physics2D.Linecast(playerPos - playerWidth, playerPos + new Vector3(0, yDir) - playerWidth, BlockingLayer);        
        RaycastHit2D diagLinecast = Physics2D.Linecast(playerPos + xDir * playerWidth, playerPos + direction, BlockingLayer);
        /*
        Vector3 s = playerPos + playerWidth;
        Vector3 en = playerPos + new Vector3(0, yDir) + playerWidth;
        Vector3 dir = (en - s);
        Debug.DrawRay(s, dir, Color.red, 1000f, false);
        
        Vector3 s2 = playerPos - playerWidth;
        Vector3 en2 = playerPos + new Vector3(0, yDir) - playerWidth;
        Vector3 dir2 = (en - s);
        Debug.DrawRay(s2, dir2, Color.red, 1000f, false);
        */

        if (xLinecast)
        {
            direction.x *= xLinecast.distance - 0.01f;
        }

        if (yLeftLinecast)
        {
            direction.y *= yLeftLinecast.distance - 0.01f;
        }

        if (!yLeftLinecast && yRightLinecast)
        {
            direction.y *= yRightLinecast.distance - 0.01f;
        }

        if (!xLinecast && !yRightLinecast && !yLeftLinecast && diagLinecast)
        {
            direction *= diagLinecast.distance - 0.01f;
        }

        transform.Translate(direction * PlayerData.MoveSpeed * Time.deltaTime);
    }
}
