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
            List<Transform> Targets = new List<Transform>();
            foreach (Transform player in Players)
            {
                if ((player.position - Enemy.StartPos).magnitude < Enemy.DetectionRange) Targets.Add(player);
            }
            if (Targets.Count == 0) ComeBackToDefensePoint();
            else
            {
                Vector3 EnemyPos = transform.position + Enemy.FeetPos;
                Transform target = null;
                float distance = -1;
                foreach (Transform potentialTarget in Targets)
                {
                    Vector3 playerFeet = potentialTarget.position + new Vector3(0, -0.465f);
                    RaycastHit2D linecast = Physics2D.Linecast(EnemyPos, playerFeet, BlockingLayer);
                    if (!linecast)
                    {
                        if (target == null)
                        {
                            target = potentialTarget;
                            distance = (EnemyPos - playerFeet).magnitude;
                        }
                        else
                        {
                            float newDistance = (EnemyPos - playerFeet).magnitude;
                            if (newDistance < distance)
                            {
                                target = potentialTarget;
                                distance = newDistance;
                            }
                        }
                    }
                }

                if (target != null)
                {
                    Enemy.Triggered = true;
                    StraightToPoint(target.position + new Vector3(0, -0.465f), Enemy.AttackRange);
                }
                else
                {
                    if (!Enemy.Triggered) return;
                    Pathfinding.Pathfinding script = GetComponent<Pathfinding.Pathfinding>();
                    foreach (Transform potentialTarget in Targets)
                    {
                        script.FindPathToPos(potentialTarget.position + new Vector3(0, -0.465f));
                        int length = Enemy.Path.Count;
                        if (length < Enemy.DetectionRange && length != 0)
                        {
                            FollowPath();
                            return;
                        }
                    }
                }
            }
            
            
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
        
        private void FollowPath()
        {
            Node nextNode = Enemy.Path[0];
            Vector2 displacement =
                (nextNode.position - (Vector2) transform.position).normalized * Time.deltaTime *
                Enemy.MoveSpeed;
            transform.Translate(displacement, Space.World);
            if (((Vector2) transform.position - nextNode.position).magnitude <= 0.05)
                Enemy.Path.Remove(nextNode);
        }

        public void Create(Enemy enemy, List<Transform> players, LayerMask blockingLayer)
        {
            Enemy = enemy;
            Players = players;
            BlockingLayer = blockingLayer;
        }
    }
}