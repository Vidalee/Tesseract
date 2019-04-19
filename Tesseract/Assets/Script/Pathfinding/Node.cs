using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Pathfinding
{
    public class Node: IHeapNode
    {
        public Vector2 position;
        public HashSet<Node> Neighbors;
        public Node Parent { get; set; }
        public float DistanceToPlayer { get; set; }
        //public float Heuristic { get; set; } // Hypothétique distance optimal (sans obstacle) entre l'ennemi et le joueur.
        public float DistanceToEnemy;
    
        public Node(int x, int y, Vector2 position)
        {
            this.position = position;
            Neighbors = new HashSet<Node>();
        }

        public int Comparable()
        {
            return (int) DistanceToPlayer * 100;
        }
    }
}