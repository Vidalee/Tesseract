using System;
using Script.Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;


namespace Script.Enemies
{
    public class EnemiesMovement : MonoBehaviour
    {
        [SerializeField] protected GameObject Player;
        [SerializeField] protected int Speed;
        public LayerMask BlockingLayer;
        
        private void Start()
        {
            Player = GameObject.Find("Player");
        }
 
        private void FixedUpdate()
        {
            Displacement();
        }
    
        private void Displacement()
        {
            if ((Player.transform.position - transform.position).magnitude > 2)
            {
                RaycastHit2D linecast = Physics2D.Linecast(Player.transform.position, transform.position, BlockingLayer);
                if (!linecast)
                {
                    transform.Translate((Player.transform.position - transform.position).normalized *Time.deltaTime * Speed);
                }
                else
                {
                    Node nextNode = Pathfinding.Pathfinding.Path[0];
                    Vector2 displacement =
                        (nextNode.position - (Vector2) transform.position).normalized * Time.deltaTime * Speed;
                    transform.Translate(displacement, Space.World);
                    if (((Vector2) transform.position - nextNode.position).magnitude <= 0.05)
                        Pathfinding.Pathfinding.Path.Remove(nextNode);
                }
            }            
        }
    }
}