using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using Script.GlobalsScript;
using Script.Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


namespace Script.Enemies
{
    public class EnemiesMovement : MonoBehaviour
    {
        [SerializeField] protected EnemyData Enemy;
        
        [SerializeField] protected List<Transform> Players;
        [SerializeField] protected List<PlayerData> playerDatas;
        private Animator _a;
        public LayerMask BlockingLayer;
        private bool _isMoving = false;
        private Transform _target;
 
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
            }

            _a.SetBool("Speed", _isMoving);
            if (_isMoving)
            {
                if (Enemy.OnHisWayBack)
                {
                    EnemyMovingAnimation(Enemy.StartPos);
                }
                else
                {
                    EnemyMovingAnimation(_target.position);
                }
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
                    Vector3 playerFeet = potentialTarget.transform.position + new Vector3(0, -0.5f);
                    RaycastHit2D linecast1 = Physics2D.Linecast(EnemyPos - new Vector3(Enemy.ColliderX / 2 + 0.001f , 0), playerFeet, BlockingLayer);
                    RaycastHit2D linecast2 = Physics2D.Linecast(EnemyPos + new Vector3(Enemy.ColliderX / 2 + 0.001f , 0), playerFeet, BlockingLayer);
                                        
                    if (!linecast1 && !linecast2) 
                    { 
                        if (target is null)
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
 
                if (!(target is null))
                {
                    Enemy.Triggered = true;
                    Enemy.OnHisWayBack = false;
                    _target = target;
                    Vector3 distanceToPos = target.transform.position + new Vector3(0, -0.5f) - transform.position;
                    if (distanceToPos.magnitude > Enemy.AttackRange)
                    {
                        _isMoving = true;
                        StraightToPoint(distanceToPos.normalized);
                    }
                    else
                    {
                        _isMoving = false;
                    }
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
                            _target = potentialTarget;
                            _isMoving = true;
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
            RaycastHit2D linecast1 = Physics2D.Linecast(pos - new Vector3(Enemy.ColliderX + 0.1f , 0), Enemy.StartPos, BlockingLayer);
            RaycastHit2D linecast2 = Physics2D.Linecast(pos + new Vector3(Enemy.ColliderX + 0.1f , 0), Enemy.StartPos, BlockingLayer);
            if (!linecast1 && !linecast2)
            {
                Vector3 distanceToPos = Enemy.StartPos - transform.position;
                if (distanceToPos.magnitude > 0.05f)
                {
                    _isMoving = true;
                    StraightToPoint(distanceToPos.normalized);
                }
                else
                {
                    _isMoving = false;
                }
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

        private void StraightToPoint(Vector3 direction)
        {
             transform.Translate(direction * Time.deltaTime * Enemy.MoveSpeed);
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

        private void EnemyMovingAnimation(Vector3 position)
        {
            if (Enemy.Name == "Archer")
            {
                _a.Play(position.x < transform.position.x ? "WalkL" : "WalkR");
            }
            else
            {
                _a.Play("WalkL");
            }
        }
        
        public void Create(EnemyData enemy, List<Transform> players, List<PlayerData> playerDatas, LayerMask blockingLayer, Animator animator)
        {
            Enemy = enemy;
            Players = players;
            this.playerDatas = playerDatas;
            BlockingLayer = blockingLayer;
            _a = animator;
        }
    }
}