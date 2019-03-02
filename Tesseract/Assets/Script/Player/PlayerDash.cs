﻿using System;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerDash : MonoBehaviour
{
    private GameObject player;
    private float cooldown;
    
    public float maxCooldown;
    public float dashDistance;
    public LayerMask blockingLayer;

    void Start()
    {
        player = GameObject.Find("Player");
        cooldown = maxCooldown;
    }

    void FixedUpdate()
    {
        Dash();
    }

    private bool DashCd()
    {
        if (cooldown >= maxCooldown) 
            return true;
        cooldown++;
        return false;
    }
    
    private void Dash()
    {
        bool dashCd = DashCd();
        if (Input.GetKey("space") && dashCd)
        {
            cooldown = 0;
            //Get direction of the dash
            Vector2 playerPos = player.transform.position;
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (cursorPos - playerPos).normalized;

            //Check if there is a wall
            RaycastHit2D hit = Physics2D.Raycast(playerPos, direction, dashDistance, blockingLayer);
            
            //Dash to the wall if there is one, or dash to the normal position
            if (!hit)
                player.transform.position = playerPos + direction * dashDistance;
            else
                player.transform.position = playerPos + direction * hit.distance;
        }
    }
}
