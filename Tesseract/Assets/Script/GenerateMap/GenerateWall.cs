using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateWall : MonoBehaviour
{
    private bool[,] _grid;
    private bool[,] _wallPos;
    public MapTextureData _mapTextureData;
    public Transform _wall;
    public Material Material;
    private int _wallTextureLength;
    public Transform Deco;
    public SimpleDecoration[] SimpleDecoration;
    
    public void Create(bool[,] grid)
    {
        _wallTextureLength = _mapTextureData.Wall.Length;
        _grid = grid;
        _wallPos = new bool[_grid.GetLength(0),_grid.GetLength(1)];
        
        InstantiateSimpleWall();
        ChooseWall();
        Torchwall();
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

    private Quaternion FindRotationObject(string wallType, out Vector2[] col, out Sprite sprite)
    {
        col = _mapTextureData.CubeCol;
        sprite = _mapTextureData.Wall[Random.Range(0, _wallTextureLength)];
        
        //Corner wall
        if (wallType.Contains("C"))
        {
            sprite = _mapTextureData.WallCorner;
            col = _mapTextureData.CornerCol;
            if (wallType.Contains("TR"))
            {
                return Quaternion.AngleAxis(-90, Vector3.forward);
            }
            if (wallType.Contains("BL"))
            {
                return Quaternion.AngleAxis(90,Vector3.forward);
            }
            if (wallType.Contains("TL"))
            {
                return Quaternion.AngleAxis(180,Vector3.forward);
            }
            
            return Quaternion.identity;
        }
        //Double wall
        if (wallType.Contains('D'))
        {
            sprite = _mapTextureData.WallDoubleSide;
            
            if (wallType.Contains("BT"))
            {
                col = _mapTextureData.DemiCol;
                return Quaternion.AngleAxis(-90, Vector3.forward);
            }
            
            return Quaternion.identity;
        }
        
        //Wall 1 side
        if (wallType.Length == 1)
        {
            sprite = _mapTextureData.Wall1Side;
            
            if (wallType.Contains("L"))
            {
                return Quaternion.AngleAxis(90,Vector3.forward);
            }
            if (wallType.Contains("T"))
            {
                return Quaternion.AngleAxis(180,Vector3.forward);
            }
            if (wallType.Contains("R"))
            { 
                return Quaternion.AngleAxis(-90,Vector3.forward);
            }
            col = _mapTextureData.WallPerspective1Col;
            return Quaternion.identity;
        }
                
        //Wall 2 side
        if (wallType.Length == 2)
        {
            sprite = _mapTextureData.Wall2Side;
            
            if (wallType.Contains("RT"))
            {
                return Quaternion.AngleAxis(-90, Vector3.forward);
            }
            if (wallType.Contains("TL"))
            {
                return Quaternion.AngleAxis(180, Vector3.forward);
            }
            if (wallType.Contains("BL"))
            {
                col = _mapTextureData.WallPerspective2Col;
                return Quaternion.AngleAxis(180,Vector3.up);
            }
            col = _mapTextureData.WallPerspective2Col;
            return Quaternion.identity;
        }
        
        //Wall 3 side
        if (wallType.Length == 3)
        {
            sprite = _mapTextureData.Wall3Side;
            if (!wallType.Contains("B"))
            {
                return Quaternion.AngleAxis(-90, Vector3.forward);
            }
            if (!wallType.Contains("T"))
            {
                col = _mapTextureData.DemiCol2;
                return Quaternion.AngleAxis(90, Vector3.forward);
            }
            if (!wallType.Contains("R"))
            {
                col = _mapTextureData.WallPerspective2Col;
                return Quaternion.AngleAxis(180, Vector3.up);
            }

            col = _mapTextureData.WallPerspective2Col;
            return Quaternion.identity;
        }
        
        //Wall 4 side
        if (wallType == "BRTL")
        {
            col = _mapTextureData.DemiCol;
            sprite = _mapTextureData.Wall4Side;
            return Quaternion.AngleAxis(-90, Vector3.forward);
        }
        
        return Quaternion.identity;
    }

    private void InstantiateWall(string wallType, int x, int y)
    {
        string[] w = wallType.Split(' ');
        
        for (int i = 0; i < w.Length; i++)
        {
            if (w[i] == "" || i > 0 && w[i].Contains('C') && (w[0].Contains(w[i][2])|| w[0].Contains(w[i][3]))) continue;
            
            Quaternion rot = FindRotationObject(w[i], out Vector2[] col, out Sprite sprite);
            Transform wall = Instantiate(_wall,new Vector3(y, x), rot, transform);
            
            if (!w[i].Contains('C'))
            {
                if (w[i].Contains('W'))
                {
                    Transform o1 = Instantiate(Deco, new Vector3(y, x - 1), rot, transform);
                    o1.GetComponent<SimpleDeco>().Create(SimpleDecoration[0]);
                }
                
                if (w[i].Contains('R'))
                {
                    Transform o = Instantiate(Deco, new Vector3(y + 1, x), Quaternion.AngleAxis(90, Vector3.forward), transform);
                    o.GetComponent<SimpleDeco>().Create(SimpleDecoration[0]);
                    
                    if (Random.Range(0, 4) == 0)
                    {
                        Transform o4 = Instantiate(Deco, new Vector3(y + 0.5f, x), Quaternion.identity, transform);
                        o4.GetComponent<SimpleDeco>().Create(SimpleDecoration[1]);
                    }
                    
                    if (w[i].Contains("BR"))
                    {
                        Transform o2 = Instantiate(Deco, new Vector3(y + 1, x + 1), Quaternion.AngleAxis(180, Vector3.forward), transform);
                        o2.GetComponent<SimpleDeco>().Create(SimpleDecoration[5]);
                    }
                }
                
                if (w[i].Contains('L'))
                {
                    Transform o = Instantiate(Deco, new Vector3(y - 1, x), Quaternion.AngleAxis(-90, Vector3.forward), transform);
                    o.GetComponent<SimpleDeco>().Create(SimpleDecoration[0]);
                    
                    if (Random.Range(0, 4) == 0)
                    {
                        Transform o4 = Instantiate(Deco, new Vector3(y - 0.5f, x), Quaternion.identity, transform);
                        o4.GetComponent<SimpleDeco>().Create(SimpleDecoration[1]);
                    }
                    
                    if (w[i].Contains("BL")  || w[i].Contains("BRL") || w[i].Contains("BRTL") ||w[i].Contains("BTL"))
                    {
                        Transform o2 = Instantiate(Deco, new Vector3(y - 1, x + 1), Quaternion.AngleAxis(-90, Vector3.forward), transform);
                        o2.GetComponent<SimpleDeco>().Create(SimpleDecoration[5]);
                    }
                }
                
                if (w[i].Contains('B'))
                {
                    Transform o = Instantiate(Deco, new Vector3(y, x + 1), Quaternion.AngleAxis(180, Vector3.forward), transform);
                    o.GetComponent<SimpleDeco>().Create(SimpleDecoration[0]);
                }
            }
            if (col == _mapTextureData.WallPerspective1Col ||
                col == _mapTextureData.WallPerspective2Col ||
                col == _mapTextureData.DemiCol ||
                sprite == _mapTextureData.WallCorner)
            {
                wall.GetComponent<SpriteRenderer>().sortingOrder = 110;
            }

            SpriteRenderer wallSpriteRenderer = wall.GetComponent<SpriteRenderer>();
            if (wallType == "WWWWWW") wallSpriteRenderer.material = Material;
            EdgeCollider2D edgeCollider2D = wall.GetComponent<EdgeCollider2D>();
            wallSpriteRenderer.sprite = sprite;
            edgeCollider2D.points = col;   
        }
    }

    private void Torchwall()
    {
        for (int x = 1; x < _wallPos.GetLength(1) - 1; x++)
        {
            for (int y = 1; y < _wallPos.GetLength(0) - 1; y++)
            {
                if (_wallPos[x, y])
                {
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
                    
                    if (Random.Range(0, 5) == 0)
                    {
                        Transform o = Instantiate(Deco, new Vector3(y, x), Quaternion.identity, transform);
                        o.GetComponent<SimpleDeco>().Create(SimpleDecoration[1]);
                    }
                }
            }
        }

    }
}
