using System;
using System.Collections.Generic;
using Script.GlobalsScript;
using Script.Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGridCreation : MonoBehaviour
{
    public int MapHeight;
    public int MapWidth;
    public int RoomNumber;
    public int DistanceRoom;

    public int maxTry;
    public int fusion;
    public int maxH;
    public int minH;
    public int maxW;
    public int minW;

    public int seed;

    public Transform floor;
    public Transform wallTexture;

    [SerializeField] protected MapTextureData MapTextureData;
    [SerializeField] protected KruskalAlgo KruskalAlgo;

    private List<RoomData> _roomData;

    private bool[,] _grid;

    private void Awake()
    {
        Random.InitState(seed);
        _grid = new bool[MapHeight, MapWidth];
        _roomData = new List<RoomData>();
        CreateGrid();
        AllNodes.Grid = _grid;
        AllNodes.Height = MapHeight - 1;
        AllNodes.Width = MapWidth - 1;
    }

    public void CreateGrid()
    {
        int index = 0;
        for (int i = 0; i < RoomNumber; i++)
        {
            if (maxTry <= 0) break;

            int height = Random.Range(minH, maxH);
            int width = Random.Range(minW, maxW);

            int x1 = Random.Range(width + 2, MapWidth - 2) - width;
            int x2 = x1 + width;

            int y1 = Random.Range(height + 2, MapHeight - 2) - height;
            int y2 = y1 + height;

            if (CheckCollision(x1 - 1, x2 + 1, y1 - 2, y2 + 2) || fusion > 0)
            {
                AddToGrid(x1, y1, x2, y2);
                RoomData newRoom = ScriptableObject.CreateInstance<RoomData>();
                newRoom.Create(x1, y1, x2, y2, height, width, index);
                _roomData.Add(newRoom);
                index++;
            }
            else
            {
                i--;
            }
        }
        
        InstantiateObject();
        WallTexture();

        Show();
        ShowStuff();
    }

    public void AddToGrid(int x1, int y1, int x2, int y2)
    {
        for (int i = y1; i < y2; i++)
        {
            for (int j = x1; j < x2; j++)
            {
                _grid[i, j] = true;
            }
        }
    }

    private bool CheckCollision(int x1, int x2, int y1, int y2)
    {
        foreach (var r in _roomData)
        {
            if (x1 <= r.X2 && x2 >= r.X1 && y1 <= r.Y2 && y2 >= r.Y1)
            {
                if (fusion > 0) fusion--;
                maxTry--;
                return false;
            }
        }

        return true;
    }

    public void BuildRoad(int[] pos1, int[] pos2)
    {

        int x1 = pos1[0];
        int x2 = pos2[0];

        int y1 = pos1[1];
        int y2 = pos2[1];

        if (Random.Range(0, 2) == 0)
        {
            BuildLineX(x1, x2, y1);
            BuildLineY(y1, y2, x2);
        }
        else
        {
            BuildLineY(y1, y2, x1);
            BuildLineX(x1, x2, y2);
        }
    }

    public void BuildLineX(int x1, int x2, int y)
    {
        if (x1 > x2)
        {
            int save = x1;
            x1 = x2;
            x2 = save;
        }

        for (int i = x1; i <= x2; i++)
        {
            _grid[y, i] = true;
        }
    }

    public void BuildLineY(int y1, int y2, int x)
    {
        if (y1 > y2)
        {
            int save = y1;
            y1 = y2;
            y2 = save;
        }

        for (int i = y1; i <= y2; i++)
        {
            _grid[i, x] = true;
            _grid[i, x + 1] = true;
        }
    }

    private IHeapNode[] CreateEdge()
    {
        HashSet<RoomData> visited = new HashSet<RoomData>();
        int vertices = _roomData.Count;
        List<IHeapNode> edges = new List<IHeapNode>();
        int maxDistance = DistanceRoom * DistanceRoom;

        for (int i = 0; i < _roomData.Count; i++)
        {
            if (visited.Add(_roomData[i]))
            {
                for (int j = i + 1; j < vertices; j++)
                {
                    if (!visited.Contains(_roomData[j]))
                    {
                        int distance = (int) (Math.Pow(_roomData[i].Center[0] - _roomData[j].Center[0], 2)
                                              + Math.Pow(_roomData[i].Center[1] - _roomData[j].Center[1], 2));

                        if (distance > maxDistance) continue;

                        Edge edge = new Edge();
                        edge.Weight = distance;
                        edge.Source = _roomData[i].Index;
                        edge.Destination = _roomData[j].Index;
                        edges.Add(edge);
                    }
                }
            }
        }

        return edges.ToArray();
    }

    private Edge[] LinkRoom()
    {
        IHeapNode[] edges = CreateEdge();
        
        //ShowGraph(edges);
        
        Graph graph = KruskalAlgo.CreateGraph(_roomData.Count, edges);
        Edge[] road = KruskalAlgo.Kruskal(graph);
        return road;
    }
    
    public void ShowGraph(IHeapNode[] edges)
    {
        Edge[] edges = LinkRoom();

        foreach (var e in edges)
        {            
            BuildRoad(_roomData[e.Source].Center, _roomData[e.Destination].Center); 
        }
        
        FillGap();
        
        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetLength(1); j++)
            {
                if (_grid[i, j])
                {
                    Transform o = Instantiate(floor, new Vector3(j, i),
                        Quaternion.AngleAxis(Random.Range(0,3) * 90,Vector3.forward),transform);
                    o.GetComponent<SpriteRenderer>().sprite =
                        MapTextureData.Floor[Random.Range(0, MapTextureData.Floor.Length)];
                }
            }
        }
    }

    private void FillGap()
    {
        for (int i = 1; i < _grid.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < _grid.GetLength(1) - 1; j++)
            {
                if (_grid[i, j]) continue;

                if (_grid[i + 1, j] && _grid[i - 1, j]) _grid[i, j] = true;
                if (_grid[i + 1, j - 1] && _grid[i - 1, j - 1]) _grid[i, j - 1] = true;
                if (_grid[i + 1, j + 1] && _grid[i - 1, j + 1]) _grid[i, j + 1] = true;
            }
        }
    }

    private void WallTexture()
    {
        Transform o = Instantiate(wallTexture, transform.position, Quaternion.identity);
        GenerateWall script = o.GetComponent<GenerateWall>();
        
        script.Create(_grid);
    }

    public void ShowGraph(IHeapNode[] edges)
    {
        foreach (var e in edges)
        {
            int[] p1 = _roomData[((Edge) e).Source].Center;
            int[] p2 = _roomData[((Edge) e).Destination].Center;

            Vector3 s = new Vector3(p1[0], p1[1]);
            Vector3 en = new Vector3(p2[0], p2[1]);
            Vector3 dir = (en - s);

            //Debug.Log(e.Source + "->" + e.Destination + " : " + Mathf.Sqrt(e.Weight));
            
            BuildRoad(_roomData[e.Source].Center, _roomData[e.Destination].Center);
            
            Debug.DrawRay(s, dir, Color.red, 1000f, false);
        }
    }
    
    public void ShowMST(Edge e)
    {
        int[] p1 = _roomData[e.Source].Center;
        int[] p2 = _roomData[e.Destination].Center;

        Vector3 s = new Vector3(p1[0], p1[1]);
        Vector3 en = new Vector3(p2[0], p2[1]);
        Vector3 dir = (en - s);
            
        Debug.DrawRay(s, dir, Color.red, 1000f, false);
    }
}