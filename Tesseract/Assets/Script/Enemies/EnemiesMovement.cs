using System;
using System.IO;
using System.Numerics;
using Script.Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


namespace Script.Enemies
{
    public class EnemiesMovement : MonoBehaviour
    {
        [SerializeField] protected Transform PlayerFeet;
        [SerializeField] protected int Speed;
        public LayerMask BlockingLayer;
        
 
        private void FixedUpdate()
        {
            Displacement();
        }
    
        private void Displacement()
        {
            Vector3 enemyFeetPos = transform.position - new Vector3(0, transform.localScale.y / 2, 0);
            if ((PlayerFeet.position - enemyFeetPos).magnitude > 2)
            {
                if (!Physics2D.Linecast(enemyFeetPos, PlayerFeet.position, BlockingLayer))
                {
                    transform.Translate((PlayerFeet.position - transform.position).normalized * Time.deltaTime * Speed);
                }
                else if (Pathfinding.Pathfinding.Path.Count != 0)
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