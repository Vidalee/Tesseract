using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Pathfinding
{
      public class Pathfinding : MonoBehaviour
      {                  
            public GameObject Enemy;
            public static Node start;
            public Node destination;
            public static List<Node> Path = new List<Node>();
                    
            public void reconstructPath()
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
                  foreach (Node node in AllNodes.nodes)
                  {
                        node.DistanceToEnemy = float.MaxValue;
                        node.DistanceToPlayer = float.MaxValue;
                  }
                  start.DistanceToEnemy = 0;
                  start.DistanceToPlayer = Math.Abs((destination.position - start.position).magnitude);
                  //TODO SortedList<Node> openList = new SortedList<Node>();
                  /*
                  openList.Put(start);
                  HashSet<Node> VisitedNodes = new HashSet<Node>();
                  while (!openList.IsEmpty())
                  {
                        Node node = openList.Take();
                        if (node == destination)
                        {
                              reconstructPath();
                              return;
                        }
                        foreach (Node neighbor in node.Neighbors)
                        {
                              float newDistance = node.DistanceToEnemy + Math.Abs((node.position - neighbor.position).magnitude);
                              if (neighbor.DistanceToEnemy <= newDistance) continue;
                              if (VisitedNodes.Remove(neighbor))
                              {
                                    neighbor.Heuristic =  Math.Abs((destination.position - node.position).magnitude);
                              }
                              neighbor.DistanceToEnemy = newDistance;
                              neighbor.DistanceToPlayer = newDistance + neighbor.Heuristic;
                              neighbor.Parent = node;
                              openList.Put(neighbor);
                        }
                        VisitedNodes.Add(node);
                  }
                  throw new Exception("AStar pas de chemin");
                  */
            }

            private void Update()
            {
                  if (AllNodes.PlayerPositionChanged)
                  {
                        AllNodes.PlayerPositionChanged = false;
                        // TODO start = AllNodes.FindNode(Enemy.transform.position);
                        destination = AllNodes.PlayerNode;
                        AStar();
                  }
            }
      }
}