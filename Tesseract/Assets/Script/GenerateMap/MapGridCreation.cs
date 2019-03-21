using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditorInternal;
using UnityEngine;

public class MapGridCreation : MonoBehaviour
{
    public int MapHeight;
    public int MapWidth;
    public int RoomNumber;

    public int maxtry;
    public int fusion;
    public int maxH;
    public int minH;
    public int maxW;
    public int minW;

    public Transform floor;
    public Transform wall;

    private List<RoomData> _roomData;
    private bool[,] _grid;

    private void Start()
    {
        _grid = new bool[MapHeight, MapWidth];
        _roomData = new List<RoomData>();
        CreateGrid();
    }

    public void CreateGrid()
    {
        for (int i = 0; i < RoomNumber; i++)
        {
            if (maxtry <= 0) return;

            int height = Random.Range(minH, maxH);
            int width = Random.Range(minW, maxW);

            int x1 = Random.Range(width, MapWidth) - width;
            int x2 = x1 + width;

            int y1 = Random.Range(height, MapHeight) - height;
            int y2 = y1 + height;

            if (CheckCollision(x1 - 1, x2 + 1, y1 - 1, y2 + 1) || fusion > 0)
            {
                AddToGrid(x1, y1, x2, y2);
                RoomData newRoom = ScriptableObject.CreateInstance<RoomData>();
                newRoom.Create(x1, y1, x2, y2, height, width);
                _roomData.Add(newRoom);
            }
            else
            {
                i--;
            }
        }

        Debug.Log("RoomDone");
        BuildRoad();
        Debug.Log("RoadDone");
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

    public bool CheckCollision(int x1, int x2, int y1, int y2)
    {
        foreach (var r in _roomData)
        {
            if (x1 <= r.X2 && x2 >= r.X1 && y1 <= r.Y2 && y2 >= r.Y1)
            {
                if (fusion > 0) fusion--;
                maxtry--;
                return false;
            }
        }

        return true;
    }

    public void ShowStuff()
    {
        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetLength(1); j++)
            {
                if (_grid[i, j]) Instantiate(floor, new Vector3(j, i), Quaternion.identity);
            }
        }

        Debug.Log("Done");
    }

    public void BuildRoad()
    {
        for (int i = 1; i < _roomData.Count; i++)
        {
            int[] center1 = _roomData[i - 1].Center;
            int[] center2 = _roomData[i].Center;

            int x1 = center1[0];
            int x2 = center2[0];

            int y1 = center1[1];
            int y2 = center2[1];

            Instantiate(wall, new Vector3(x1, y1), Quaternion.identity);
            Instantiate(wall, new Vector3(x2, y2), Quaternion.identity);

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
    }

    public void BuildLineX(int x1, int x2, int y)
    {
        if (x1 > x2)
        {
            int save = x1;
            x1 = x2;
            x2 = save;
        }

        Debug.Log("X");
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
        
        Debug.Log("Y");
        for (int i = y1; i <= y2; i++)
        {
            _grid[i, x] = true;
        }
    }

}
