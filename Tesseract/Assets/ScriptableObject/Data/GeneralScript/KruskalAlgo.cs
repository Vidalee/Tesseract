using UnityEngine;

namespace Script.GlobalsScript
{
    public struct Graph
    {
        public int VerticesCount;
        public IHeapNode[] Edge;
 
    }

    public class Edge : IHeapNode
    {
        public int Source;
        public int Destination;
        public int Weight;
        
        public int Comparable() => Weight;
    }

    public struct Subset
    {
        public int Parent;
        public int rank;
    }
    [CreateAssetMenu(fileName = "KruskaAlgo", menuName = "GeneralScript/KruskalAlgo")]
    public class KruskalAlgo : ScriptableObject
    {

        [SerializeField] protected UnionFind UnionFind;
        [SerializeField] protected BinaryHeap BinaryHeap;

        public Graph CreateGraph(int verticesCount, IHeapNode[] edges)
        {
            Graph graph = new Graph();
            graph.VerticesCount = verticesCount;
            graph.Edge = edges;

            return graph;
        }
     
        public Edge[] Kruskal(Graph graph)
        {
            int verticesCount = graph.VerticesCount;
            Edge[] result = new Edge[verticesCount - 1];
            int e = 0;
            int i = 0;

            Subset[] subset = new Subset[verticesCount];

            BinaryHeap.CreateMaxHeap(graph.Edge);
            BinaryHeap.MinHeapSort(graph.Edge);
            
            for(int v = 0; v < verticesCount; v++)
            {
                subset[v].Parent = v;
                subset[v].rank = 0;
            }

            while (e < verticesCount - 1)
            {
                Edge nextEdge = (Edge) graph.Edge[i++];
                int x = UnionFind.Find(subset, nextEdge.Source);
                int y = UnionFind.Find(subset, nextEdge.Destination);

                if (x != y)
                {
                    result[e++] = nextEdge;
                    UnionFind.Union(subset, x, y);
                }
            }

            return result;
        }
    }
}