﻿using System.Globalization;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerData _playerData;
    [SerializeField] protected LayerMask BlockingLayer;
    [SerializeField] protected GameEvent PlayerMoveEvent;

    public void Create(PlayerData playerData)
    {
        _playerData = playerData;
    }
    
    private void Update()
    {
        PlayerMove();
    }
    
    private void PlayerMove()
    {
        if (!_playerData.CanMove)
        {
            return;
        }
        
        int xDir = (int) Input.GetAxisRaw("Horizontal");
        int yDir = (int) Input.GetAxisRaw("Vertical");
        
        PlayerMoveEvent.Raise(new EventArgsCoor(xDir, yDir));

        if (xDir == 0 && yDir == 0) return;
        
        Vector3 playerPos = transform.position;
        playerPos.y += -_playerData.Height / 2;
        if (yDir > 0) playerPos.y += _playerData.FeetHeight;
        Vector3 playerWidth = new Vector3(_playerData.Width / 2, 0, 0);
        Vector3 direction = new Vector3(xDir,yDir,0);

        RaycastHit2D xLinecast = Physics2D.Linecast(playerPos + xDir * playerWidth, playerPos + xDir * playerWidth + new Vector3(xDir, 0), BlockingLayer);
        RaycastHit2D yLeftLinecast = Physics2D.Linecast(playerPos + playerWidth, playerPos + playerWidth + new Vector3(0, yDir), BlockingLayer);
        RaycastHit2D yRightLinecast = Physics2D.Linecast(playerPos - playerWidth, playerPos - playerWidth + new Vector3(0, yDir), BlockingLayer);
        
        if (xLinecast)
        {
            direction.x *= xLinecast.distance - 0.01f;
        }

        if (yLeftLinecast)
        {
            direction.y *= yLeftLinecast.distance - 0.01f;
        }
        else if (yRightLinecast)
        {
            direction.y *= yRightLinecast.distance - 0.01f;
        }

        if (!xLinecast && !yRightLinecast && !yLeftLinecast)
        {
            RaycastHit2D diagLinecastLeft = Physics2D.Linecast(playerPos + playerWidth, playerPos + playerWidth + direction, BlockingLayer);
            RaycastHit2D diagLinecastRight = Physics2D.Linecast(playerPos - playerWidth, playerPos - playerWidth + direction, BlockingLayer);
            
            /* DEBUG
            Vector3 s = playerPos - playerWidth;
            Vector3 en = playerPos - playerWidth + direction;
            Vector3 dir3 = (en - s);
            Debug.DrawRay(s, dir3, Color.red, 1000f, false);
            */

            if (diagLinecastLeft && diagLinecastRight)
            {
                if (diagLinecastLeft.distance < diagLinecastRight.distance)
                {
                    direction *= diagLinecastLeft.distance - 0.01f;
                }
                else
                {
                    direction *= diagLinecastRight.distance - 0.01f;
                }
            }
            else
            {
                if (diagLinecastLeft) direction *= diagLinecastLeft.distance - 0.01f;
                else if(diagLinecastRight) direction *= diagLinecastRight.distance - 0.01f;
            }
        }

        transform.Translate(direction * _playerData.MoveSpeed * Time.deltaTime);
    }
}