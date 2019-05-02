using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class GenerateWall : MonoBehaviour
{
    private bool[,] _grid;
    private bool[,] _wallPos;
    public MapTextureData MapTextureData;
    public Transform Wall;
    private int _wallTextureLength;
    public Transform Deco;
    public SimpleDecoration[] SimpleDecoration;

    private Tilemap _wallMap;
    private Tilemap _perspMap;
    private Tilemap _floorMap;
    private Tilemap _shadWMap;
    private Tilemap[] _shadCMap;
    private Tilemap[] _shadSMap;
    private Tilemap _miniMap;
    private Tile _tile;
    
    
    public void Create(bool[,] grid, Tilemap wallMap, Tilemap perspMap, Tilemap colMap, Tilemap shadMap,
        Tilemap[] shadSMap, Tilemap[] shadCMap, Tilemap miniMap)
    {
        _wallTextureLength = MapTextureData.Wall.Length;
        _grid = grid;
        _wallPos = new bool[_grid.GetLength(0),_grid.GetLength(1)];
        
        _wallMap = wallMap;
        _perspMap = perspMap;
        _wallMap = colMap;
        _shadWMap = shadMap;
        _shadCMap = shadCMap;
        _shadSMap = shadSMap;
        _miniMap = miniMap;
        
        _tile = ScriptableObject.CreateInstance<Tile>();
        
        InstantiateSimpleWall();
        ChooseWall();
        CornerShadow();
        RefreshCollision();
    }

    private void InstantiateSimpleWall()
    {
        for (int i = _grid.GetLength(1) - 2; i > 0; i--)
        {
            for (int j = _grid.GetLength(0) - 2; j > 0; j--)
            {
                if(_grid[i, j]) continue;
                
                if (_grid[i - 1, j])
                {
                    string wallType = "WWWWWW";
                    InstantiateWall(wallType, i, j);
                    _grid[i, j] = true;
                    _wallPos[i, j] = true;
                }
            }
        }
    }

    private void ChooseWall()
    {
        for (int i = _grid.GetLength(1) - 2; i > 0; i--)
        {
            for (int j = _grid.GetLength(0) - 2; j > 0; j--)
            {
                if(_grid[i, j]) continue;
                
                string wallType = "";

                if (_grid[i + 1, j]) wallType += "B";
                if (_grid[i, j + 1]) wallType += "R";
                if (_grid[i - 1, j]) wallType += "T";
                if (_grid[i, j - 1]) wallType += "L";

                if (wallType.Equals("BT") || wallType.Equals("RL")) wallType += "D";

                if (_grid[i + 1, j + 1]) wallType += " CCBR";
                if (_grid[i - 1, j - 1]) wallType += " CCTL";
                if (_grid[i + 1, j - 1]) wallType += " CCBL";
                if (_grid[i - 1, j + 1]) wallType += " CCTR";


                if (wallType == "") continue;
                
                InstantiateWall(wallType, i , j);
            }
        }
    }

    private Sprite FindRotationObject(string wallType)
    {
        //Corner wall
        if (wallType.Contains("C"))
        {            
            if (wallType.Contains("TR"))
            {
                return MapTextureData.WallCorner[1];
            }
            
            if (wallType.Contains("BL"))
            {
                return MapTextureData.WallCorner[3];
            }
            
            if (wallType.Contains("TL"))
            {
                return MapTextureData.WallCorner[2];
            }
            
            return MapTextureData.WallCorner[0];
        }
        //Double wall
        if (wallType.Contains('D'))
        {
            
            if (wallType.Contains("BT"))
            {
                return MapTextureData.WallDoubleSide[1];
            }
            
            return MapTextureData.WallDoubleSide[0];
        }
        
        //Wall 1 side
        if (wallType.Length == 1)
        {            
            if (wallType.Contains("L"))
            {
                return MapTextureData.Wall1Side[3];
            }
            if (wallType.Contains("T"))
            {
                return MapTextureData.Wall1Side[2];
            }
            if (wallType.Contains("R"))
            { 
                return MapTextureData.Wall1Side[1];
            }
            return MapTextureData.Wall1Side[0];
        }
                
        //Wall 2 side
        if (wallType.Length == 2)
        {            
            if (wallType.Contains("RT"))
            {
                return MapTextureData.Wall2Side[1];
            }
            if (wallType.Contains("TL"))
            {
                return MapTextureData.Wall2Side[2];
            }
            if (wallType.Contains("BL"))
            {
                return MapTextureData.Wall2Side[3];
            }
            return MapTextureData.Wall2Side[0];
        }
        
        //Wall 3 side
        if (wallType.Length == 3)
        {
            if (!wallType.Contains("B"))
            {
                return MapTextureData.Wall3Side[1];
            }
            if (!wallType.Contains("T"))
            {
                return MapTextureData.Wall3Side[3];
            }
            if (!wallType.Contains("R"))
            {
                return MapTextureData.Wall3Side[2];
            }

            return MapTextureData.Wall3Side[0];
        }
        
        //Wall 4 side
        if (wallType == "BRTL")
        {
            return MapTextureData.Wall4Side;
        }
        
        return MapTextureData.Wall[Random.Range(0, _wallTextureLength)];
    }

    private void RefreshCollision()
    {
        Tilemap[] tilemaps = {_wallMap, _perspMap};
        
        foreach (var maps in tilemaps)
        {
            TilemapCollider2D col = maps.GetComponent<TilemapCollider2D>();

            col.enabled = false;
            col.enabled = true;
            
            col.composite.GenerateGeometry();
        }
    }
    
    private void InstantiateWall(string wallType, int x, int y)
    {
        string[] w = wallType.Split(' ');
        
        for (int i = 0; i < w.Length; i++)
        {
            if (w[i] == "" || i > 0 && w[i].Contains('C') && (w[0].Contains(w[i][2])|| w[0].Contains(w[i][3]))) continue;
            
            _tile.sprite = MapTextureData.MiniMap[1];
            _miniMap.SetTile(new Vector3Int(y, x, 0), _tile);
            
            _tile.sprite =  FindRotationObject(w[i]);

            if (w[i].Contains("W"))
            {
                _wallMap.SetTile(new Vector3Int(y, x, 0), _tile);
            }
            else if (w[i].Contains('C'))
            {
                Quaternion rot = Quaternion.identity;

                if (_tile.sprite == MapTextureData.WallCorner[1]) rot = Quaternion.AngleAxis(-90, Vector3.forward);
                else if (_tile.sprite == MapTextureData.WallCorner[2]) rot = Quaternion.AngleAxis(180, Vector3.forward);
                else if (_tile.sprite == MapTextureData.WallCorner[3]) rot = Quaternion.AngleAxis(90, Vector3.forward);

                Instantiate(Wall, new Vector3(y, x), rot, transform);
            }
            else
            {
                _perspMap.SetTile(new Vector3Int(y, x, 0), _tile);
            }
            
            if (!w[i].Contains('C'))
            {
                if (w[i].Contains('W'))
                {
                    _tile.sprite = MapTextureData.ShadowSide[0];
                    _shadSMap[0].SetTile(new Vector3Int(y, x - 1, 0), _tile);
                }
                
                if (w[i].Contains('R'))
                {
                    CreateTorch(new Vector3(y + 0.5f, x), SimpleDecoration[1]);
                    
                    _tile.sprite = MapTextureData.ShadowSide[3];
                    _shadSMap[3].SetTile(new Vector3Int(y + 1, x, 0), _tile);
                    
                    if (w[i].Contains("BR"))
                    {              
                        _tile.sprite = MapTextureData.ShadowCorner[2];
                        _shadCMap[2].SetTile(new Vector3Int(y + 1, x + 1, 0), _tile);
                    }
                }
                
                if (w[i].Contains('L'))
                {
                    CreateTorch(new Vector3(y - 0.5f, x), SimpleDecoration[2]);
                    
                    _tile.sprite = MapTextureData.ShadowSide[1];
                    _shadSMap[1].SetTile(new Vector3Int(y - 1, x, 0), _tile);
                    
                                        
                    if (w[i].Contains("BL")  || w[i].Contains("BRL") || w[i].Contains("BRTL") ||w[i].Contains("BTL"))
                    {
                        _tile.sprite = MapTextureData.ShadowCorner[1];
                        _shadCMap[1].SetTile(new Vector3Int(y - 1, x + 1, 0), _tile);
                    }
                }
                
                if (w[i].Contains('B'))
                {
                    _tile.sprite = MapTextureData.ShadowSide[2];
                    _shadSMap[2].SetTile(new Vector3Int(y, x + 1, 0), _tile);
                }
            }
        }
    }

    private void CornerShadow()
    {
        for (int x = 1; x < _wallPos.GetLength(1) - 1; x++)
        {
            for (int y = 1; y < _wallPos.GetLength(0) - 1; y++)
            {
                if (_wallPos[x, y])
                {
                    CreateTorch(new Vector3(y, x), SimpleDecoration[0]);

                    _tile.sprite = MapTextureData.ShadowWall;
                    _shadWMap.SetTile(new Vector3Int(y, x, 0), _tile);
                    
                    if (!_wallPos[x, y + 1])
                    {
                        _tile.sprite = MapTextureData.ShadowSide[3];
                        _shadSMap[3].SetTile(new Vector3Int(y + 1, x, 0), _tile);
                        
                        if (!_wallPos[x - 1, y + 1])
                        {
                            _tile.sprite = MapTextureData.ShadowCorner[3];
                            _shadCMap[3].SetTile(new Vector3Int(y + 1, x - 1, 0), _tile);
                        }
                    }

                    if (!_wallPos[x, y - 1])
                    {
                        _tile.sprite = MapTextureData.ShadowSide[1];
                        _shadSMap[1].SetTile(new Vector3Int(y - 1, x, 0), _tile);

                        if (!_wallPos[x - 1, y - 1])
                        {
                            _tile.sprite = MapTextureData.ShadowCorner[0];
                            _shadCMap[0].SetTile(new Vector3Int(y - 1, x - 1, 0), _tile);
                        }
                    }
                }
            }
        }

    }

    private void CreateTorch(Vector3 pos, SimpleDecoration deco)
    {
        if (Random.Range(0, 5) == 0)
        {
            Transform o4 = Instantiate(Deco, pos, Quaternion.identity, transform);
            o4.GetComponent<SimpleDeco>().Create(deco);
        }
    }
}
