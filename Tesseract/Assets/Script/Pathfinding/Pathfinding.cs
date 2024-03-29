﻿using System;
using System.Collections.Generic;
using Script.GlobalsScript;
using UnityEngine;

namespace Script.Pathfinding
{
      public class Pathfinding : MonoBehaviour
      {
            private static Node start;
            public Node destination;
            [SerializeField] protected EnemyData Enemy;

            public void ReconstructPath()
            {
                  Enemy.Path.Clear();
                  Enemy.Path.Add(destination);
                  while (Enemy.Path[0].Parent != start)
                  {
                        if (Enemy.Path[0].Parent == null) return; 
                        Enemy.Path.Insert(0, Enemy.Path[0].Parent);
                  }
            }
            
            public void AStar()
            {
                  //Debug.Log("Start");
                  if (start == null || destination == null)
                  {
                        //Debug.Log("Null");
                        return;
                  }   
                  foreach (Node node in AllNodes.NodesGrid)
                  {
                        if (node != null)
                        {
                              node.DistanceToEnemy = float.MaxValue;
                              node.DistanceToPlayer = float.MaxValue;
                        }
                  }
                  start.DistanceToEnemy = 0;
                  start.DistanceToPlayer = Math.Abs((destination.position - start.position).magnitude);

                  int lastIndex = 1;
                  List<IHeapNode> openList = new List<IHeapNode>();
                  BinaryHeap.MinPush(openList, start);
                   
                  while (lastIndex != 0)
                  {
                        Node node = (Node) BinaryHeap.MinPop(openList);
                        lastIndex--;
                        
                        if (node == destination)
                        {
                              ReconstructPath();
                              //Debug.Log("Found it");
                              return;
                        }
                        foreach (Node neighbor in node.Neighbors)
                        {
                              float newDistance = node.DistanceToEnemy + Math.Abs((node.position - neighbor.position).magnitude);
                              if (neighbor.DistanceToEnemy <= newDistance) continue;
                              neighbor.DistanceToEnemy = newDistance;
                              neighbor.DistanceToPlayer = newDistance + Math.Abs((destination.position - node.position).magnitude);
                              neighbor.Parent = node;
                              BinaryHeap.MinPush(openList, neighbor);
                              lastIndex++;
                        }
                  }
                  //Debug.Log("No path");
            }

            
            public void FindPathToPlayer(Transform player)
            {
                  PlayerData playerData = player.parent.GetComponent<PlayerManager>().PlayerData;
                  if (playerData.PositionChanged)
                  {
                        start = AllNodes.PositionToNode(transform.position);
                        destination = playerData.Node;
                        AStar();
                  }
            }
            
            
            public void FindPathToPos(Vector3 position)
            {
                  start = AllNodes.PositionToNode(transform.position);
                  destination = AllNodes.PositionToNode(position);
                  AStar();    
            }

            public void Create(EnemyData enemy)
            {
                  Enemy = enemy;
            }
      }
}