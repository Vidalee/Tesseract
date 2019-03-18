
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
    
    private Animator _animator;
    
    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void PlayerMove()
    {
        int xDir = (int) Input.GetAxisRaw("Horizontal");
        int yDir = (int) Input.GetAxisRaw("Vertical");
        
        PlayerMoveEvent.Raise();

        if (xDir == 0 && yDir == 0) return;
        
        Vector3 xVec = new Vector3(xDir,0,0);
        Vector3 yVec = new Vector3(0,yDir,0);
        
        Vector3 playerPos = transform.position;
        playerPos.y += PlayerData.Height/2 + PlayerData.FeetHeight/2;
        playerPos.x += xVec.x * PlayerData.Width;
        
        RaycastHit2D xLinecast = Physics2D.Linecast(playerPos, playerPos + xVec, BlockingLayer);
        RaycastHit2D yLinecast = Physics2D.Linecast(playerPos, playerPos + yVec, BlockingLayer);

        Vector3 direction = new Vector3(xDir,yDir,0);

        if (xLinecast)
        {
            direction.x *= xLinecast.distance - PlayerData.Width - 0.01f;
        }

        if (yLinecast)
        {
            direction.y *= yLinecast.distance - PlayerData.FeetHeight - 0.01f;
        }
        
        transform.Translate(direction * PlayerData.MoveSpeed * Time.deltaTime);
    }
}
