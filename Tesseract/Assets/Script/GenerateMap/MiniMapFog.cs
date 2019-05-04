using Script.GlobalsScript;
using Script.GlobalsScript.Struct;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MiniMapFog : MonoBehaviour
{
    private Tilemap _miniMapCam;
    private Tile _tile;
    private bool[,] _grid;
    private bool[,] _render;
    private int _height;
    private int _width;
    private MapTextureData _mapTextureData;

    public void Create(Tilemap miniMap, bool[,] grid, MapTextureData mapTextureData)
    {
        
        _grid = grid;
        _miniMapCam = miniMap;
        _tile = ScriptableObject.CreateInstance<Tile>();
        _mapTextureData = mapTextureData;
        _height = _grid.GetLength(0);
        _width = _grid.GetLength(1);
        _render = new bool[_height, _width];
    }

    public void RevealMap(IEventArgs args)
    {
        EventArgsCoor coor = (EventArgsCoor) args;

        for (int x = coor.X - 10; x < 10 + coor.X; x++)
        {
            for (int y = coor.Y - 10; y < 10 + coor.Y; y++)
            {
                if (x < 0 || x > _width - 1 || y < 0 || y > _height - 1 || _render[y, x]) continue;
                
                _render[y, x] = true;
                _tile.sprite = _mapTextureData.MiniMap[_grid[y, x] ? 0 : 1];
                Vector3Int pos = new Vector3Int(x, y, 0);
                _miniMapCam.SetTile(pos, _tile);
            }
        }
    }
}
