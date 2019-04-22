using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Pathfinding
{
    public class AllNodes : MonoBehaviour
    {
        public static bool[,] Grid;
        public static int Height;
        public static int Width;
        public static Node[,] NodesGrid;

        public static List<Transform> players = new List<Transform>();
        [SerializeField] private List<PlayerData> playersData = new List<PlayerData>();
        private int _playersNbr;
        
        private void Start()
        {
            GraphCreation();
            foreach (Transform player in players)
            {
                PlayerData playerData = player.parent.GetComponent<PlayerManager>().GetPlayerData;
                playerData.Node = PositionToNode(player.transform.position);
                playersData.Add(playerData);
            }

            _playersNbr = players.Count;
        }

        private void Update()
        {
            foreach (Transform player in players)
            {
                PlayerData playerData = player.parent.GetComponent<PlayerManager>().GetPlayerData;
                Node newNode = PositionToNode(player.transform.position);
                playerData.PositionChanged = newNode != playerData.Node;
                playerData.Node = newNode;
            }
        }

        private void GraphCreation()
        {
            NodesGrid = new Node[Height + 1, Width + 1];
            for (int h = 1; h < Height; h++)
            { 
                for (int w = 1; w < Width; w++)
                 {
                     if (Grid[h, w])
                     {
                         NodesGrid[h,w] = new Node(new Vector2(w, h - 0.5f));
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
            }
            ShowGraph();
        }

        private void ShowGraph()
        {
            int nbrVoisins = 0;
            int nbrNoeuds = 0;
            foreach (Node node in NodesGrid)
            {
                if (node != null)
                {
                    nbrNoeuds++;
                    foreach (Node neighbor in node.Neighbors)
                    {
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
            int w = (int) (position.x + 0.5);
            int h = (int) (position.y + 0.8);
            if (h < 0 || h > Height || w < 0 || w > Width) return null;
            return NodesGrid[h,w]; 
        }
    }
}