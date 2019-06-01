using System;
using System.Collections.Generic;
using Script.GlobalsScript;
using Script.GlobalsScript.Struct;
using Script.Pathfinding;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapGridCreation : MonoBehaviour
{

    public Transform Player;
    public Transform PlayerMulti;
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
    public int forcePath;
    public int prob;
    public int portalChance;

    public int simpleDecoration;
    public int seed;

    public Transform wallTexture;
    public Transform room;
    
    public Tilemap FloorMap;
    public Tilemap PerspMap;
    public Tilemap WallMap;
    public Tilemap ShadWMap;
    public Tilemap MiniMap;
    public Tilemap[] ShadSMap;
    public Tilemap[] ShadCornMap;
    
    
    [SerializeField] protected MapTextureData MapTextureData;

    private List<RoomData> _roomData;
    private List<Transform> _rooms;

    private bool[,] _grid;
    private bool[,] _instances;
    
    private void Awake()
    {
        Coffre.Créer();
        Random.InitState(seed);
        _grid = new bool[MapHeight, MapWidth];
        _instances = new bool[MapHeight, MapWidth];
        _roomData = new List<RoomData>();
        _rooms = new List<Transform>();
        AllNodes.Grid = _grid;
        AllNodes.Height = MapHeight - 1;
        AllNodes.Width = MapWidth - 1;
        
        GetComponentInChildren<MiniMapFog>().Create(MiniMap, _grid, MapTextureData);

        CreateGrid();
        ConstructCorridor();
        
        RoomInstanceWall();

        FillGap();
        CreateFloor();
        CreateWall();

        RoomInstanceDeco();
        AddChest();
        AddPikes();
        
        AddPortal();
        if((string) Coffre.Regarder("mode") == "solo") AddPlayer(1, true);

        GenerateEnemies.RoomData = _roomData;
        GenerateEnemies.availablePosGrid = _grid;
    }

    public bool[,] Instances => _instances;
    
    //Create grid and room in it
    private void CreateGrid()
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
                _rooms.Add(CreateRoom(x1, y1, newRoom));
                
                index++;
            }
            else
            {
                i--;
            }
        }
    }

    //Add true to the grid (set a room)
    private void AddToGrid(int x1, int y1, int x2, int y2)
    {
        for (int i = y1; i < y2; i++)
        {
            for (int j = x1; j < x2; j++)
            {
                _grid[i, j] = true;
            }
        }
    }

    //Add false/true on one pos (from room)
    public void AddToGrid(int x, int y, bool state)
    {
        _grid[x, y] = state;
        _instances[x, y] = true;
    }

    public void AddToInstance(int x, int y, bool state, bool col)
    {
        _instances[x, y] = state;
        _grid[x, y] = col;
    }

    //Collision between room
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
    
    //Create room
    private Transform CreateRoom(int x, int y, RoomData roomData)
    {
        Transform o = Instantiate(room, new Vector3(x, y), Quaternion.identity, transform);
        o.GetComponent<RoomInstance>().Create(roomData, prob);
        return o;
    }
    
    //Call instance in room
    private void RoomInstanceWall()
    {
        for (int i = 0; i < _rooms.Count; i++)
        {
            RoomInstance script = _rooms[i].GetComponent<RoomInstance>();
            script.BigWall();
        }
    }
    
    //Call instance deco in room
    private void RoomInstanceDeco()
    {
        for (int i = 0; i < _rooms.Count; i++)
        {
            RoomInstance script = _rooms[i].GetComponent<RoomInstance>();
            script.AddSimpleDecoration(Random.Range(0, simpleDecoration));
        }
    }
    
    //Call chest in room
    private void AddChest()
    {
        for (int i = 0; i < _rooms.Count; i++)
        {
            RoomInstance script = _rooms[i].GetComponent<RoomInstance>();
            script.AddChest();
        }
    }

    //Call portal in room
    private void AddPortal()
    {
        for (int i = 0; i < _rooms.Count; i++)
        {
            if (Random.Range(0, portalChance + 1) == 0)
            {
                RoomInstance script = _rooms[i].GetComponent<RoomInstance>();
                Vector3 pos = _rooms[Random.Range(0, _rooms.Count)].GetComponent<RoomInstance>().GetFreePos();
                
                if(pos == Vector3.zero) continue;
                
                script.AddPortal(pos);
            }
        }
    }
    
    //Call pikes in room
    private void AddPikes()
    {
        for (int i = 0; i < _rooms.Count; i++)
        {
            RoomInstance script = _rooms[i].GetComponent<RoomInstance>();
            script.AddPikes();
        }   
    }
    
    //Add player
    public void AddPlayer(int id, bool solo)
    {
        int j = 0;
        while (j < 100)
        {
            int i = Random.Range(0, _roomData.Count);
            RoomData roomData = _roomData[i];
            
            int x = roomData.X1 + Random.Range(1, roomData.Width - 2);
            int y = roomData.Y1 + Random.Range(1, roomData.Height - 2);
        
            if (!Instances[y, x] && _grid[y, x])
            {
                Instantiate(Player, new Vector3(0, 0), Quaternion.identity).GetComponent<PlayerManager>().Create(x, y, id, solo);
                return;
            }

            j++;
        }
    }

    //Add multi player
    public void AddMultiPlayer(int id)
    {
        int j = 0;
        while (j < 100)
        {
            int i = Random.Range(0, _roomData.Count);
            RoomData roomData = _roomData[i];

            int x = roomData.X1 + Random.Range(1, roomData.Width - 2);
            int y = roomData.Y1 + Random.Range(1, roomData.Height - 2);

            if (!Instances[y, x] && _grid[y, x])
            {
                Instantiate(PlayerMulti, new Vector3(0, 0), Quaternion.identity).GetComponent<PlayerManagerMulti>().Create(x, y, id);
                return;
            }

            j++;
        }
    }
    //Build road between 2 position
    private void BuildRoad(int[] pos1, int[] pos2)
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

    //Build X line
    private void BuildLineX(int x1, int x2, int y)
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

    //Build Y line
    private void BuildLineY(int y1, int y2, int x)
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

    //Create link between close room for the MST algo
    private List<IHeapNode> CreateEdge()
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

        return edges;
    }
    
    //Create road from list of edge
    private void ConstructCorridor()
    {
        List<IHeapNode> edges = CreateEdge();
        
        //ShowGraph(edges);
        
        Graph graph = KruskalAlgo.CreateGraph(_roomData.Count, edges);
        Edge[] road = KruskalAlgo.Kruskal(graph);

        foreach (var e in road)
        {
            //ShowMst(e);
            BuildRoad(_roomData[e.Source].Center, _roomData[e.Destination].Center); 
        }

        int size = _roomData.Count;
        for (int i = 0; i < forcePath; i++)
        {
            BuildRoad(_roomData[Random.Range(0, size)].Center, _roomData[Random.Range(0, size)].Center);
        }
    }

    //Instantiate floor with the bool grid
    private void CreateFloor()
    {
        FloorMap.GetComponent<Renderer>().sortingOrder = MapHeight * -105;
        
        Tile tile = ScriptableObject.CreateInstance<Tile>();

        int len = MapTextureData.Floor.Length;
        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetLength(1); j++)
            {
                if (_grid[i, j])
                {
                    tile.sprite = MapTextureData.Floor[Random.Range(0, len)];
                    FloorMap.SetTile(new Vector3Int(j, i, 0), tile);
                }
            }
        }
    }

    //Fill gap for legit wall
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

    //Call the wall texture sprite and give the grid
    private void CreateWall()
    {
        Transform o = Instantiate(wallTexture, transform.position, Quaternion.identity, transform);
        GenerateWall script = o.GetComponent<GenerateWall>();
        
        WallMap.GetComponent<Renderer>().sortingOrder = MapHeight * -105;
        script.Create(_grid, FloorMap, PerspMap, WallMap, ShadWMap, ShadSMap, ShadCornMap);
    }

    //Debug show graph before mst
    private void ShowGraph(List<IHeapNode> edges)
    {
        foreach (var e in edges)
        {
            int[] p1 = _roomData[((Edge) e).Source].Center;
            int[] p2 = _roomData[((Edge) e).Destination].Center;

            Vector3 s = new Vector3(p1[0], p1[1]);
            Vector3 en = new Vector3(p2[0], p2[1]);
            Vector3 dir = (en - s);

            Debug.DrawRay(s, dir, Color.red, 1000f, false);
        }
    }
    
    //Debug show graph after mst
    private void ShowMst(Edge e)
    {
        int[] p1 = _roomData[e.Source].Center;
        int[] p2 = _roomData[e.Destination].Center;

        Vector3 s = new Vector3(p1[0], p1[1]);
        Vector3 en = new Vector3(p2[0], p2[1]);
        Vector3 dir = (en - s);
            
        Debug.DrawRay(s, dir, Color.red, 1000f, false);
    }
}