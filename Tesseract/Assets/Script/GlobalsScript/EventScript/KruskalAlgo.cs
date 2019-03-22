using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class KruskalAlgo : MonoBehaviour
 {
     public Graph CreateGraph(int verticesCount, int edgesCount)
     {
         Graph graph = new Graph();
         graph.VerticesCount = verticesCount;
         graph.EdgeCounts = edgesCount;
         graph.Edge = new Edge[edgesCount];

         return graph;
     }

     public int Find(Subset[] subset, int i)
     {
         if (subset[i].Parent != i)
             subset[i].Parent = Find(subset, subset[i].Parent);

         return subset[i].Parent;
     }
 }
 
 public struct Graph
 {
     public int EdgeCounts;
     public int VerticesCount;
     public Edge[] Edge;
 
 }
 
 public struct Edge
 {
     public int Sources;
     public int Destination;
     public int Weight;
 }
 
 public struct Subset
 {
     public int Parent;
     public int rank;
 }
 
