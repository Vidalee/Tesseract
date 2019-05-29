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
        [SerializeField] protected EnemyData Enemy;
        
        [SerializeField] protected List<Transform> Players;
        [SerializeField] protected List<PlayerData> playerDatas;
        
        public LayerMask BlockingLayer;
        
 
        private void FixedUpdate()
        {
            bool TriggeredState = Enemy.Triggered;
            FindTarget();
            if (TriggeredState != Enemy.Triggered)
            {
                int maxLvl = 0;
                foreach (PlayerData playerData in playerDatas)
                {
                    maxLvl = playerData.Lvl > maxLvl ? playerData.Lvl : maxLvl;
                }

                Enemy.UpdateStats(maxLvl, 1); //TODO Récupérer l'étage
            }
        }
        
        private void FindTarget()
        {
            List<Transform> Targets = new List<Transform>();
            foreach (Transform player in Players)
            {
                if ((player.transform.position - Enemy.StartPos).magnitude < Enemy.DetectionRange) Targets.Add(player);
            }
            if (Targets.Count == 0)
            {
                if (Enemy.Triggered) ComeBackToDefensePoint();
            }
            else
            {
                Vector3 EnemyPos = transform.position;
                Transform target = null;
                float distance = -1;
                foreach (Transform potentialTarget in Targets)
                {
                    Vector3 playerFeet = potentialTarget.transform.position + new Vector3(0, -0.375f);
                    RaycastHit2D linecast = Physics2D.Linecast(EnemyPos, playerFeet, BlockingLayer);
                    Debug.DrawRay(EnemyPos, playerFeet - EnemyPos, Color.green);
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
                    Enemy.OnHisWayBack = false;
                    StraightToPoint(target.transform.position + new Vector3(0, -0.375f), Enemy.AttackRange);
                }
                else
                {
                    if (!Enemy.Triggered) return;
                    Pathfinding.Pathfinding script = GetComponent<Pathfinding.Pathfinding>();
                    foreach (Transform potentialTarget in Targets)
                    {
                        script.FindPathToPlayer(potentialTarget);
                        int length = Enemy.Path.Count;
                        if (length < Enemy.DetectionRange && length != 0)
                        {
                            Enemy.OnHisWayBack = false;
                            FollowPath();
                            return;
                        }
                    }
                }
            }
            
            
        }

        private void ComeBackToDefensePoint()
        {
            Vector3 pos = transform.position;
            RaycastHit2D linecast = Physics2D.Linecast(pos, Enemy.StartPos, BlockingLayer);

            if (!linecast)
            {
                StraightToPoint(Enemy.StartPos, 0.05f);
            }
            else
            {
                if (!Enemy.OnHisWayBack)
                {
                    Enemy.OnHisWayBack = true;
                    GetComponent<Pathfinding.Pathfinding>().FindPathToPos(Enemy.StartPos);
                }
                if (Enemy.Path.Count != 0)
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
            Vector3 distanceToPos = position - transform.position;
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

        public void Create(EnemyData enemy, List<Transform> players, List<PlayerData> playerDatas, LayerMask blockingLayer)
        {
            Enemy = enemy;
            Players = players;
            this.playerDatas = playerDatas;
            BlockingLayer = blockingLayer;
        }
    }
}