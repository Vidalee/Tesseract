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

        public static Node PlayerNode;
        public static bool PlayerPositionChanged;

        private void Start()
        {
            GraphCreation();
        }

        private void GraphCreation()
        {
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
                    //Instantiate(Floor).transform.position = node.position;
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

        public static Node PositionToNode(Vector2 position)
        {
            return nodesGrid[(int) position.x, (int) position.y];
        }
    }
}