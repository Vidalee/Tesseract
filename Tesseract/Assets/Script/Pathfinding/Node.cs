﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Pathfinding
{
    public class Node: IComparable<Node>
    {
        public Vector2 position;
        public HashSet<Node> Neighbors;
        public Node Parent { get; set; }
        public float DistanceToPlayer { get; set; }
        public float Heuristic { get; set; } // Hypothétique distance optimal (sans obstacle) entre l'ennemi et le joueur.
        public float DistanceToEnemy { get; set; } 
    
        public Node(Vector2 position)
        {
            this.position = position;
            Neighbors = new HashSet<Node>();
        }

        public Node FindNeighbor(Vector2 coordinates)
        {
            foreach (Node node in Neighbors)
            {
                if (node.position == coordinates) return node;
            }
            throw new Exception("FindNeighbor : Le noeud ne fait pas partie des voisins");
        }

        public bool IsNeighbor(Vector2 coordinates)
        {
            foreach (Node node in Neighbors)
            {
                if (node.position == coordinates) return true;
            }
            return false;
        }
        
        public int CompareTo(Node other)
        {
            if (DistanceToPlayer < other.DistanceToPlayer) return -1;
            if (Math.Abs(DistanceToPlayer - other.DistanceToPlayer) < Mathf.Epsilon) return 0;
            return 1;
        }
    }
}