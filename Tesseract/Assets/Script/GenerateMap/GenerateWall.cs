using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateWall : MonoBehaviour
{
    private bool[,] _grid;
    public MapTextureData _mapTextureData;
    public Transform _wall;
    private int _wallTextureLength;
    
    public void Create(bool[,] grid)
    {
        _wallTextureLength = _mapTextureData.Wall.Length;
        _grid = grid;
        
        InstantiateSimpleWall();
        ChooseWall();
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
            
            Transform wall = Instantiate(_wall,new Vector3(y, x), FindRotationObject(w[i], out Vector2[] col, out Sprite sprite), transform);
            if (col == _mapTextureData.WallPerspective1Col || col == _mapTextureData.WallPerspective2Col || sprite == _mapTextureData.WallCorner)
            {
                wall.GetComponent<SpriteRenderer>().sortingOrder = 110;
            }
    
            SpriteRenderer wallSpriteRenderer = wall.GetComponent<SpriteRenderer>();
            EdgeCollider2D edgeCollider2D = wall.GetComponent<EdgeCollider2D>();
            wallSpriteRenderer.sprite = sprite;
            edgeCollider2D.points = col;   
        }
    }
}
