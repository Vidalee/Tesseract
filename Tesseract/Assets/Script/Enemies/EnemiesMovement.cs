using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using Script.Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


namespace Script.Enemies
{
    public class EnemiesMovement : MonoBehaviour
    {
        [SerializeField] protected Enemy Enemy;
        
        [SerializeField] protected List<Transform> Players;
        
        public LayerMask BlockingLayer;
        
 
        private void FixedUpdate()
        {
            FindTarget();
        }
        
        private void FindTarget()
        {
            List<Transform> Targets = Players.OrderBy(player => (player.position - Enemy.StartPos).magnitude).Select().ToList();
        }

        private void ComeBackToDefensePoint()
        {
            Vector3 pos = transform.position + Enemy.FeetPos;
            RaycastHit2D linecast = Physics2D.Linecast(pos, Enemy.StartPos, BlockingLayer);

            if (!linecast)
            {
                StraightToPoint(Enemy.StartPos, 0.05f);
            }
            else
            {
                GetComponent<Pathfinding.Pathfinding>().FindPathToPos(Enemy.StartPos);
                int pathLength = Enemy.Path.Count;
                if (pathLength != 0)
                {
                    Node nextNode = Enemy.Path[0];
                    Vector2 displacement =
                        (nextNode.position - (Vector2) transform.position).normalized * Time.deltaTime *
                        Enemy.MoveSpeed;
                    transform.Translate(displacement, Space.World);
                    if (((Vector2) transform.position - nextNode.position).magnitude <= 0.05) 
                        Enemy.Path.Remove(nextNode);
                }
            }
        }

        private void StraightToPoint(Vector3 position, float range)
        {
            Vector3 distanceToPos = position - transform.position + Enemy.FeetPos;
            if (distanceToPos.magnitude > range)
            {
                transform.Translate(distanceToPos.normalized * Time.deltaTime * Enemy.MoveSpeed);
            }
        }
        
        
        
        private void Displacement(Transform target)
        {
            Vector3 playerFeet = target.position + new Vector3(0, -0.465f);
            Vector3 defensePointToPlayer = playerFeet - Enemy.StartPos;
            if (defensePointToPlayer.magnitude < Enemy.DetectionRange)
            {
                Vector3 Pos = transform.position + Enemy.FeetPos;
                RaycastHit2D linecast = Physics2D.Linecast(Pos, playerFeet, BlockingLayer);
                Enemy.Triggered = Enemy.Triggered || !linecast;
                if (!Enemy.Triggered) return;

                if (!linecast)
                {
                    Vector3 DistanceToPlayer = playerFeet - Pos;
                    if (DistanceToPlayer.magnitude > Enemy.AttackRange)
                    {
                        transform.Translate(DistanceToPlayer.normalized * Time.deltaTime * Enemy.MoveSpeed);
                    }
                }
                else
                {
                    GetComponent<Pathfinding.Pathfinding>().FindPathToPlayer();
                    int pathLength = Enemy.Path.Count;
                    if (pathLength != 0 && pathLength < Enemy.DetectionRange)
                    {
                        Node nextNode = Enemy.Path[0];
                        Vector2 displacement =
                            (nextNode.position - (Vector2) transform.position).normalized * Time.deltaTime *
                            Enemy.MoveSpeed;
                        transform.Translate(displacement, Space.World);
                        if (((Vector2) transform.position - nextNode.position).magnitude <= 0.05)
                            Enemy.Path.Remove(nextNode);
                    }
                }
            }
        }

        public void Create(Enemy enemy, List<Transform> players, LayerMask blockingLayer)
        {
            Enemy = enemy;
            Players = players;
            BlockingLayer = blockingLayer;
        }
    }
}