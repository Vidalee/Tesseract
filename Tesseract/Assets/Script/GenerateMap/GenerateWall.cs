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
    public Material Material;
    private int _wallTextureLength;
    public Transform Deco;
    public SimpleDecoration[] SimpleDecoration;

    private Tilemap _wallMap;
    private Tilemap _perspMap;
    private Tilemap _shadMap;
    private Tilemap _colMap;
    
    public void Create(bool[,] grid, Tilemap wallMap, Tilemap perspMap, Tilemap colMap, Tilemap shadMap)
    {
        _wallTextureLength = MapTextureData.Wall.Length;
        _grid = grid;
        _wallPos = new bool[_grid.GetLength(0),_grid.GetLength(1)];
        _wallMap = wallMap;
        _perspMap = perspMap;
        _colMap = colMap;
        _shadMap = shadMap;
        
        InstantiateSimpleWall();
        ChooseWall();
        CornerShadow();
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

    private Sprite FindRotationObject(string wallType, out Vector2[] col)
    {
        col = MapTextureData.CubeCol;

        //Corner wall
        if (wallType.Contains("C"))
        {
            col = MapTextureData.CornerCol;
            
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
                col = MapTextureData.DemiCol;
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
            col = MapTextureData.WallPerspective1Col;
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
                col = MapTextureData.WallPerspective2Col;
                return MapTextureData.Wall2Side[3];
            }
            col = MapTextureData.WallPerspective2Col;
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
                col = MapTextureData.DemiCol2;
                return MapTextureData.Wall3Side[3];
            }
            if (!wallType.Contains("R"))
            {
                col = MapTextureData.WallPerspective2Col;
                return MapTextureData.Wall3Side[2];
            }

            col = MapTextureData.WallPerspective2Col;
            return MapTextureData.Wall3Side[0];
        }
        
        //Wall 4 side
        if (wallType == "BRTL")
        {
            col = MapTextureData.DemiCol;
            return MapTextureData.Wall4Side;
        }
        
        return MapTextureData.Wall[Random.Range(0, _wallTextureLength)];
    }

    private void InstantiateWall(string wallType, int x, int y)
    {
        string[] w = wallType.Split(' ');
        
        for (int i = 0; i < w.Length; i++)
        {
            if (w[i] == "" || i > 0 && w[i].Contains('C') && (w[0].Contains(w[i][2])|| w[0].Contains(w[i][3]))) continue;
            
            Tile tile = ScriptableObject.CreateInstance<Tile>();
            tile.sprite = FindRotationObject(w[i], out Vector2[] col);
            
            if (w[i].Contains("W"))
            {
                _wallMap.SetTile(new Vector3Int(y, x, 0), tile);
            }
            else if (col == MapTextureData.CornerCol)
            {
                Transform wall = Instantiate(Wall,new Vector3(y, x), Quaternion.identity, transform);
                wall.GetComponent<SpriteRenderer>().sprite = tile.sprite;
            }
            else
            {
                _perspMap.SetTile(new Vector3Int(y, x, 0), tile);
            }
            
            //Transform wall = Instantiate(_wall,new Vector3(y, x), Quaternion.identity, transform);
            
            if (!w[i].Contains('C'))
            {
                /*
                if (w[i].Contains('W'))
                {
                    Transform o1 = Instantiate(Deco, new Vector3(y, x - 1), Quaternion.identity, transform);
                    o1.GetComponent<SimpleDeco>().Create(SimpleDecoration[0]);
                }
                */
                
                if (w[i].Contains('R'))
                {
                    CreateTorch(new Vector3(y + 0.5f, x), SimpleDecoration[1]);
                    
                    /*
                    Transform o = Instantiate(Deco, new Vector3(y + 1, x), Quaternion.AngleAxis(90, Vector3.forward), transform);
                    o.GetComponent<SimpleDeco>().Create(SimpleDecoration[0]);
                    
                    if (w[i].Contains("BR"))
                    {
                        Transform o2 = Instantiate(Deco, new Vector3(y + 1, x + 1), Quaternion.AngleAxis(180, Vector3.forward), transform);
                        o2.GetComponent<SimpleDeco>().Create(SimpleDecoration[5]);
                    }
                    */
                }
                
                if (w[i].Contains('L'))
                {
                    CreateTorch(new Vector3(y - 0.5f, x), SimpleDecoration[2]);
                    
                    /*
                    Transform o = Instantiate(Deco, new Vector3(y - 1, x), Quaternion.AngleAxis(-90, Vector3.forward), transform);
                    o.GetComponent<SimpleDeco>().Create(SimpleDecoration[0]);
                                        
                    if (w[i].Contains("BL")  || w[i].Contains("BRL") || w[i].Contains("BRTL") ||w[i].Contains("BTL"))
                    {
                        Transform o2 = Instantiate(Deco, new Vector3(y - 1, x + 1), Quaternion.AngleAxis(-90, Vector3.forward), transform);
                        o2.GetComponent<SimpleDeco>().Create(SimpleDecoration[5]);
                    }
                    */
                }
                
                /*
                if (w[i].Contains('B'))
                {
                    Transform o = Instantiate(Deco, new Vector3(y, x + 1), Quaternion.AngleAxis(180, Vector3.forward), transform);
                    o.GetComponent<SimpleDeco>().Create(SimpleDecoration[0]);
                }
                */
            }

            //SpriteRenderer wallSpriteRenderer = wall.GetComponent<SpriteRenderer>();
            //wallSpriteRenderer.sortingOrder = x * -100;
            //if (wallType == "WWWWWW") wallSpriteRenderer.material = Material;
            //EdgeCollider2D edgeCollider2D = wall.GetComponent<EdgeCollider2D>();
            //wallSpriteRenderer.sprite = sprite;
            //edgeCollider2D.points = col;   
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

                    /*
                     Transform o4 = Instantiate(Deco, new Vector3(y, x), Quaternion.identity, transform);
                    o4.GetComponent<SimpleDeco>().Create(SimpleDecoration[4]);
                    
                    if (!_wallPos[x, y + 1])
                    {
                        Transform o2 = Instantiate(Deco, new Vector3(y + 1, x), Quaternion.AngleAxis(90, Vector3.forward), transform);
                        o2.GetComponent<SimpleDeco>().Create(SimpleDecoration[0]);
                        
                        if (!_wallPos[x - 1, y + 1])
                        {
                            Transform o3 = Instantiate(Deco, new Vector3(y + 1, x - 1), Quaternion.AngleAxis(90, Vector3.forward), transform);
                            o3.GetComponent<SimpleDeco>().Create(SimpleDecoration[5]);
                        }
                    }

                    if (!_wallPos[x, y - 1])
                    {
                        Transform o3 = Instantiate(Deco, new Vector3(y - 1, x), Quaternion.AngleAxis(-90, Vector3.forward), transform);
                        o3.GetComponent<SimpleDeco>().Create(SimpleDecoration[0]);
                        
                        if (!_wallPos[x - 1, y - 1])
                        {
                            Transform o2 = Instantiate(Deco, new Vector3(y - 1, x - 1), Quaternion.AngleAxis(0, Vector3.forward), transform);
                            o2.GetComponent<SimpleDeco>().Create(SimpleDecoration[5]);
                        }
                    }
                    */
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
