using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Script.Pathfinding
{
    public class AllNodes : MonoBehaviour
    {
        public static bool[,] grid;
        public static int height;
        public static int width;
        public static Node[,] nodesGrid; 
        public static List<Node> nodes = new List<Node>();

        public GameObject Player;
        public static Node PlayerNode;
        public static int NodeRadius = 1;
        private static int NodeSize = 2;
        public static bool PlayerPositionChanged;

        
        //TODO
        public GameObject Floor;

        private void Start()
        {
            Debug.Log(Time.realtimeSinceStartup);
            GraphCreation();
            Debug.Log(Time.realtimeSinceStartup);
        }

        private void GraphCreation()
        {
            /*
            bool[,] grid =
            {
                {true, true, false},
                {false, true, true},
                {true, true, true}
            };
            int height = 3 - 1;
            int width = 3 - 1;
            */
            
            nodesGrid = new Node[height + 1, width + 1];
            if (grid[0, 0]) nodesGrid[0,0] = new Node(0, 0, new Vector2(0, 0));
            for (int w = 1; w <= width; w++)
                if (grid[0, w])
                {
                    nodesGrid[0,w] = new Node(w, 0, new Vector2(w, 0));
                    if (grid[0, w - 1])
                    {
                        nodesGrid[0, w - 1].Neighbors.Add(nodesGrid[0, w]);
                        nodesGrid[0, w].Neighbors.Add(nodesGrid[0, w - 1]);
                    }

                }
            for (int h = 1; h <= height; h++)
            {
                if (grid[h, 0])
                {
                    nodesGrid[h,0] = new Node(0, h, new Vector2(0, h));
                    if (grid[h - 1, 0])
                    {
                        nodesGrid[h - 1, 0].Neighbors.Add(nodesGrid[h, 0]);
                        nodesGrid[h, 0].Neighbors.Add(nodesGrid[h - 1, 0]);
                    }

                    if (grid[h - 1, 1] && grid[h - 1,0] && grid[h,1])
                    {
                        nodesGrid[h - 1, 1].Neighbors.Add(nodesGrid[h, 0]);
                        nodesGrid[h, 0].Neighbors.Add(nodesGrid[h - 1, 1]);
                    }
                }
                for (int w = 1; w < width; w++)
                                 {
                                     if (grid[h, w])
                                     {
                                         nodesGrid[h,w] = new Node(w, h, new Vector2(w, h));
                                         if (grid[h - 1, w - 1] && grid[h - 1, w] && grid[h,w - 1])
                                         {
                                             nodesGrid[h - 1, w - 1].Neighbors.Add(nodesGrid[h, w]);
                                             nodesGrid[h, w].Neighbors.Add(nodesGrid[h - 1, w - 1]);
                                         }
                                         if (grid[h - 1, w])
                                         {
                                             nodesGrid[h - 1, w].Neighbors.Add(nodesGrid[h, w]);
                                             nodesGrid[h, w].Neighbors.Add(nodesGrid[h - 1, w]);
                                         }
                                         if (grid[h - 1, w + 1] && grid[h - 1, w] && grid[h, w + 1])
                                         {
                                             nodesGrid[h - 1, w + 1].Neighbors.Add(nodesGrid[h, w]);
                                             nodesGrid[h, w].Neighbors.Add(nodesGrid[h - 1, w + 1]);
                                         }
                                         if (grid[h, w - 1])
                                         {
                                             nodesGrid[h, w - 1].Neighbors.Add(nodesGrid[h, w]);
                                             nodesGrid[h, w].Neighbors.Add(nodesGrid[h, w - 1]);
                                         }
                                     }
                                 }
                if (grid[h, width])
                {
                    nodesGrid[h,width] = new Node(width, h, new Vector2(width, h));
                    if (grid[h - 1, width - 1] && grid[h - 1, width] && grid[h, width - 1])
                    {
                        nodesGrid[h - 1, width - 1].Neighbors.Add(nodesGrid[h, width]);
                        nodesGrid[h, width].Neighbors.Add(nodesGrid[h - 1, width - 1]);
                    }
                    if (grid[h - 1, width])
                    {
                        nodesGrid[h - 1, width].Neighbors.Add(nodesGrid[h, width]);
                        nodesGrid[h, width].Neighbors.Add(nodesGrid[h - 1, width]);
                    }
                    if (grid[h, width - 1])
                    {
                        nodesGrid[h, width - 1].Neighbors.Add(nodesGrid[h, width]);
                        nodesGrid[h, width].Neighbors.Add(nodesGrid[h, width - 1]);
                    }
                }
            }
            //ShowGraph();
        }


        private void ShowGraph()
        {
            int nbrVoisins = 0;
            int nbrNoeuds = 0;
            foreach (Node node in nodesGrid)
            {
                if (node != null)
                {
                    //print(node.position);
                    nbrNoeuds++;
                    Instantiate(Floor).transform.position = node.position;
                    foreach (Node neighbor in node.Neighbors)
                    {
                        //print(" --> " + neighbor.position);
                        Debug.DrawRay(node.position, neighbor.position - node.position, Color.red, 1000f, false);
                        nbrVoisins++;
                    }
                }
            }
            print(nbrNoeuds);
            print(nbrVoisins);

        }
        
        
        /*
        public static void AddNode(Vector2 position)
        {
            Node newNode = new Node(position + Vector2.one);
            nodes.Add(newNode);
        }

        public static void CreateLinksBetweenNodes()
        {
            foreach (Node node in nodes)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        if (x == 0 && y == 0) continue;
                        Vector2 newPosistion = new Vector2(node.position.x + x * NodeSize, node.position.y + y * NodeSize);
                        if (!node.IsNeighbor(newPosistion))
                        {
                            if (DoesNeighborExist(newPosistion, out Node neighbor))
                            {
                                if ((x + y) % 2 != 0 || DoesNeighborExist(new Vector2(node.position.x + x * NodeSize, 
                                        node.position.y), out Node neighbor1) 
                                    && DoesNeighborExist(new Vector2(node.position.x, node.position.y + y * NodeSize), 
                                        out Node neighbor2))
                                {
                                    node.Neighbors.Add(neighbor);
                                    neighbor.Neighbors.Add(node);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Update()
        {
            if (PlayerNode != null)
            {
                Vector2 lastNodePos = PlayerNode.position;
                PlayerNode = FindNode(Player.transform.position);
                PlayerPositionChanged = lastNodePos != PlayerNode.position;
            }
            else PlayerNode = FindNode(Player.transform.position);
        }

        private static bool DoesNeighborExist(Vector2 position, out Node neighbor)
        {
            foreach (Node node in nodes)
            {
                if (node.position == position)
                {
                    neighbor = node;
                    return true;
                }
            }

            neighbor = null;
            return false;
        }
        
        public static Node FindNode(Vector2 position)
        {
            float xNodePos = position.x + NodeRadius - Math.Abs(position.x % NodeSize);
            float yNodePos = position.y + NodeRadius - Math.Abs(position.y % NodeSize);
            foreach (Node node in nodes)
                if (node.position.x - NodeRadius < xNodePos && node.position.x + NodeRadius > xNodePos && node.position.y - NodeRadius < yNodePos && node.position.y + NodeRadius > yNodePos) return node;
            throw new Exception("FindNode : this shouldn't be happening");
        }
        */
    }
}