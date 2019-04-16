using UnityEngine;

namespace Script.Pathfinding
{
    public class AllNodes : MonoBehaviour
    {
        public static bool[,] Grid;
        public static int Height;
        public static int Width;
        public static Node[,] NodesGrid;

        public GameObject Player;
        public static Node PlayerNode;
        public static bool PlayerPositionChanged;

        private void Start()
        {
            GraphCreation();
            PlayerNode = PositionToNode(Player.transform.position);
        }

        private void Update()
        {
            Node newPlayerNode = PositionToNode(Player.transform.position);
            PlayerPositionChanged = newPlayerNode != PlayerNode;
            PlayerNode = newPlayerNode;
        }

        private void GraphCreation()
        {
            NodesGrid = new Node[Height + 1, Width + 1];
            if (Grid[0, 0]) NodesGrid[0,0] = new Node(0, 0, new Vector2(0, 0));
            for (int w = 1; w <= Width; w++)
                if (Grid[0, w])
                {
                    NodesGrid[0,w] = new Node(w, 0, new Vector2(w, 0));
                    if (Grid[0, w - 1])
                    {
                        NodesGrid[0, w - 1].Neighbors.Add(NodesGrid[0, w]);
                        NodesGrid[0, w].Neighbors.Add(NodesGrid[0, w - 1]);
                    }

                }
            for (int h = 1; h <= Height; h++)
            {
                if (Grid[h, 0])
                {
                    NodesGrid[h,0] = new Node(0, h, new Vector2(0, h));
                    if (Grid[h - 1, 0])
                    {
                        NodesGrid[h - 1, 0].Neighbors.Add(NodesGrid[h, 0]);
                        NodesGrid[h, 0].Neighbors.Add(NodesGrid[h - 1, 0]);
                    }

                    if (Grid[h - 1, 1] && Grid[h - 1,0] && Grid[h,1])
                    {
                        NodesGrid[h - 1, 1].Neighbors.Add(NodesGrid[h, 0]);
                        NodesGrid[h, 0].Neighbors.Add(NodesGrid[h - 1, 1]);
                    }
                }
                for (int w = 1; w < Width; w++)
                 {
                     if (Grid[h, w])
                     {
                         NodesGrid[h,w] = new Node(w, h, new Vector2(w, h));
                         if (Grid[h - 1, w - 1] && Grid[h - 1, w] && Grid[h,w - 1])
                         {
                             NodesGrid[h - 1, w - 1].Neighbors.Add(NodesGrid[h, w]);
                             NodesGrid[h, w].Neighbors.Add(NodesGrid[h - 1, w - 1]);
                         }
                         if (Grid[h - 1, w])
                         {
                             NodesGrid[h - 1, w].Neighbors.Add(NodesGrid[h, w]);
                             NodesGrid[h, w].Neighbors.Add(NodesGrid[h - 1, w]);
                         }
                         if (Grid[h - 1, w + 1] && Grid[h - 1, w] && Grid[h, w + 1])
                         {
                             NodesGrid[h - 1, w + 1].Neighbors.Add(NodesGrid[h, w]);
                             NodesGrid[h, w].Neighbors.Add(NodesGrid[h - 1, w + 1]);
                         }
                         if (Grid[h, w - 1])
                         {
                             NodesGrid[h, w - 1].Neighbors.Add(NodesGrid[h, w]);
                             NodesGrid[h, w].Neighbors.Add(NodesGrid[h, w - 1]);
                         }
                     }
                 }
                if (Grid[h, Width])
                {
                    NodesGrid[h,Width] = new Node(Width, h, new Vector2(Width, h));
                    if (Grid[h - 1, Width - 1] && Grid[h - 1, Width] && Grid[h, Width - 1])
                    {
                        NodesGrid[h - 1, Width - 1].Neighbors.Add(NodesGrid[h, Width]);
                        NodesGrid[h, Width].Neighbors.Add(NodesGrid[h - 1, Width - 1]);
                    }
                    if (Grid[h - 1, Width])
                    {
                        NodesGrid[h - 1, Width].Neighbors.Add(NodesGrid[h, Width]);
                        NodesGrid[h, Width].Neighbors.Add(NodesGrid[h - 1, Width]);
                    }
                    if (Grid[h, Width - 1])
                    {
                        NodesGrid[h, Width - 1].Neighbors.Add(NodesGrid[h, Width]);
                        NodesGrid[h, Width].Neighbors.Add(NodesGrid[h, Width - 1]);
                    }
                }
            }
            //ShowGraph();
        }

        private void ShowGraph()
        {
            int nbrVoisins = 0;
            int nbrNoeuds = 0;
            foreach (Node node in NodesGrid)
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
            return NodesGrid[(int) position.x, (int) position.y];
        }
    }
}