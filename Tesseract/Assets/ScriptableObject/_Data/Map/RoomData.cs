using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomData", menuName = "Map/Room/Data")]
public class RoomData : ScriptableObject
{
    #region Variable

    private int _x1;
    private int _x2;
    private int _y1;
    private int _y2;
    private int _height;
    private int _width;
    private int[] _center;
    private Transform[,] _instance;
    private int _index;

    #endregion

    #region Initialise

    public void Create(int x1, int y1, int x2, int y2, int height, int width, int index)
    {
        _instance = new Transform[height,width];
        _x1 = x1;
        _x2 = x2;
        _y1 = y1;
        _y2 = y2;
        _width = width;
        _height = height;
        _index = index;
        _center = new [] {(x1 + x2)/2, (y1 + y2)/2};
    }

    #endregion

    #region Modify

    public void ModifyGrid(int x, int y, Transform o)
    {
        _instance[x, y] = o;
    }

    #endregion

    #region Set/Get

    public int X1 => _x1;

    public int X2 => _x2;

    public int Y1 => _y1;

    public int Y2 => _y2;

    public int Height => _height;

    public int Width => _width;

    public int[] Center => _center;

    public Transform[,] Instance => _instance;

    public int Index => _index;

    #endregion
}
