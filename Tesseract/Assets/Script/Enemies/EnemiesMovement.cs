﻿using System;
using Script.Pathfinding;
using UnityEngine;


namespace Script.Enemies
{
    public class EnemiesMovement : MonoBehaviour
    {
        [SerializeField] protected GameObject player;
        [SerializeField] protected int Speed;
        
        private void Start()
        {
            player = GameObject.Find("Player");
        }
 
        private void FixedUpdate()
        {
            Displacement();
        }
    
        private void Displacement()
        {
            try
            {
                if ((player.transform.position - transform.position).magnitude > 2)
                {
                    Node nextNode = Pathfinding.Pathfinding.Path[0];
                
                    Vector2 displacement =
                        (nextNode.position - (Vector2) transform.position).normalized * Time.deltaTime * Speed;
                    transform.Translate(displacement, Space.World);
                    if (((Vector2) transform.position - nextNode.position).magnitude <= 0.05)
                    {
                        Pathfinding.Pathfinding.Path.Remove(nextNode);
                    }

                }            
            }
            catch(ArgumentOutOfRangeException)
            {
            }
        }

    }
}