using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Pathfinding
{
    public class AllNodes : MonoBehaviour
    {
        public static List<Node> nodes = new List<Node>();
        public GameObject Player;
        public static Node PlayerNode;
        public static int NodeRadius = 1;
        private static int NodeSize = 2;
        public static bool PlayerPositionChanged;


        public static void AddNode(Vector2 position)
        {
            Node newNode = new Node(position + Vector2.one);
            nodes.Add(newNode);
            Debug.Log(newNode.position);
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
        
    }
    
}

