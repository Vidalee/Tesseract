using System.Collections;
using System.Collections.Generic;
using Script.Pathfinding;
using UnityEngine;

public class FiboHeap : MonoBehaviour
{

     public class Edge
     {
          public int Source;
          public int Destination;
          public int Weight;
     }

     public class Graph
     {
          public int VerticesCount;
          public int EdgesCount;
          public Edge[] Edge;
     }
}

