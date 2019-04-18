using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Pathfinding
{
      public class Pathfinding : MonoBehaviour
      {                  
            public static Node start;
            public Node destination;
            public static List<Node> Path = new List<Node>();
                    
            public void ReconstructPath()
            {
                  Path.Clear();
                  Path.Add(destination);
                  while (Path[0].Parent != start)
                  {
                        if (Path[0].Parent == null) return; 
                        Path.Insert(0, Path[0].Parent);
                  }
            }
            
            public void AStar()
            {
                  if (start == null || destination == null) return;
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
                  BinaryHeap binaryHeap = ScriptableObject.CreateInstance<BinaryHeap>();
                  List<IHeapNode> openList = new List<IHeapNode>();
                  binaryHeap.MinPush(openList, start);
                  
                  while (lastIndex != 0)
                  {
                        Node node = (Node) binaryHeap.MinPop(openList);
                        lastIndex--;
                        
                        if (node == destination)
                        {
                              ReconstructPath();
                              return;
                        }
                        foreach (Node neighbor in node.Neighbors)
                        {
                              float newDistance = node.DistanceToEnemy + Math.Abs((node.position - neighbor.position).magnitude);
                              if (neighbor.DistanceToEnemy <= newDistance) continue;
                              neighbor.DistanceToEnemy = newDistance;
                              neighbor.DistanceToPlayer = newDistance + Math.Abs((destination.position - node.position).magnitude);
                              neighbor.Parent = node;
                              binaryHeap.MinPush(openList, neighbor);
                              lastIndex++;
                        }
                  }
            }

            private void Update()
            {
                  if (AllNodes.PlayerPositionChanged)
                  {
                        AllNodes.PlayerPositionChanged = false;
                        start = AllNodes.PositionToNode(transform.position);
                        destination = AllNodes.PlayerNode;
                        AStar();
                  }
            }
      }
}