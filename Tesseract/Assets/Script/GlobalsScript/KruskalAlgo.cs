using System;
using UnityEngine;

namespace Script.GlobalsScript
{
    public struct Graph
    {
        public int VerticesCount;
        public Edge[] Edge;
 
    }
 
    public struct Edge
    {
        public int Source;
        public int SourceCoor;
        public int Destination;
        public int DestinationCoor;
        public int Weight;
    }
 
    public struct Subset
    {
        public int Parent;
        public int rank;
    }
    
    public class KruskalAlgo : MonoBehaviour
    {

        [SerializeField] protected UnionFind UnionFind;
        private void Start()
        {
            {
                int verticesCount = 4;
                int edgesCount = 5;
                Graph graph = CreateGraph(verticesCount, edgesCount);

// Edge 0-1
                graph.Edge[0].Source = 0;
                graph.Edge[0].Destination = 1;
                graph.Edge[0].Weight = 10;

// Edge 0-2
                graph.Edge[1].Source = 0;
                graph.Edge[1].Destination = 2;
                graph.Edge[1].Weight = 6;

// Edge 0-3
                graph.Edge[2].Source = 0;
                graph.Edge[2].Destination = 3;
                graph.Edge[2].Weight = 5;

// Edge 1-3
                graph.Edge[3].Source = 1;
                graph.Edge[3].Destination = 3;
                graph.Edge[3].Weight = 15;

// Edge 2-3
                graph.Edge[4].Source = 2;
                graph.Edge[4].Destination = 3;
                graph.Edge[4].Weight = 4;

                Debug.Log("test");
             
                Kruskal(graph);
            }
        }

        public Graph CreateGraph(int verticesCount, int edgesCount)
        {
            Graph graph = new Graph();
            graph.VerticesCount = verticesCount;
            graph.Edge = new Edge[edgesCount];

            return graph;
        }
     
        public void Kruskal(Graph graph)
        {
            int verticesCount = graph.VerticesCount;
            Edge[] result = new Edge[verticesCount];
            int e = 0;
            int i = 0;

            Subset[] subset = new Subset[verticesCount];

            Array.Sort(graph.Edge, (a, b) => a.Weight.CompareTo(b.Weight));
         
            for(int v = 0; v < verticesCount; v++)
            {
                subset[v].Parent = v;
                subset[v].rank = 0;
            }

            while (e < verticesCount - 1)
            {
                Edge nextEdge = graph.Edge[i++];
                int x = UnionFind.Find(subset, nextEdge.Source);
                int y = UnionFind.Find(subset, nextEdge.Destination);

                if (x != y)
                {
                    result[e++] = nextEdge;
                    UnionFind.Union(subset, x, y);
                }
            }
         
            Print(result, e);
        }
     
        private static void Print(Edge[] result, int e)
        {
            for (int i = 0; i < e; ++i)
                Debug.Log(result[i].Source + "--" + result[i].Destination + "--"  + result[i].Weight);
        }
    }
}