using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateWall : MonoBehaviour
{

    private List<Vector2> _vertices;
    private bool[,] _grid;
    public MapTextureData _mapTextureData;
    public Transform _wall;
    
    public void Create(bool[,] grid)
    {
        _grid = grid;

        _vertices = new List<Vector2>();
        _vertices.Add(new Vector2(-0.5f, -0.5f));
        _vertices.Add(new Vector2(-0.5f, 0));
        
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
                    string wallType = "";
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

                if (wallType == "")
                {
                    if (_grid[i + 1, j + 1]) wallType = "CCBL";
                    if (_grid[i - 1, j - 1]) wallType = "CCTR";
                    if (_grid[i + 1, j - 1]) wallType = "CCBR";
                    if (_grid[i - 1, j + 1]) wallType = "CCTL";
                }

                if (wallType.Contains("BT") && wallType.Length == 2 
                    || wallType.Contains("RL") && wallType.Length == 2) wallType += "CCC";

                if (wallType == "") continue;
                
                InstantiateWall(wallType, i , j);
            }
        }
    }

    private Sprite FindWallTexture(string wallType)
    {

        switch (wallType.Length)
        {
            case 1:
                return _mapTextureData.Wall1Side;
            case 2:
                return _mapTextureData.Wall2Side;
            case 3: 
                return _mapTextureData.Wall3Side;
            case 4:
                return wallType.Contains("BRTL") ? _mapTextureData.Wall4Side : _mapTextureData.WallCorner; 
            case 5:
                return _mapTextureData.WallDoubleSide;
        }

        Sprite[] wallTexture = _mapTextureData.Wall;
        return wallTexture[Random.Range(0, wallTexture.Length)];
    }

    private Quaternion FindRotationObject(string wallType, out Vector2[] col)
    {
        col = _mapTextureData.CubeCol;

        if (wallType.Length == 5)
        {
            if(wallType.Contains("BT"))
            {
                return Quaternion.AngleAxis(90,Vector3.forward);
            }
        }
        
        if (wallType.Length == 4)
        {
            if (wallType.Contains("BRLT"))
            {
                col = _mapTextureData.WallPerspective1Col;
                return Quaternion.identity;
                
            }
            if (wallType.Contains("TL"))
            {
                return Quaternion.AngleAxis(-90,Vector3.forward);
            }
            if (wallType.Contains("TR"))
            {
                return Quaternion.AngleAxis(180,Vector3.forward);
            }
            if (wallType.Contains("BR"))
            {
                return Quaternion.AngleAxis(90,Vector3.forward);
            }
        }
        
        if (wallType.Length == 3)
        {
            if (!wallType.Contains("B"))
            {
                return Quaternion.AngleAxis(-90,Vector3.forward);
            }
            if (!wallType.Contains("T"))
            {
                col = _mapTextureData.WallPerspective1Col;
                return Quaternion.AngleAxis(90,Vector3.forward);
            }
            if (!wallType.Contains("R"))
            {
                return Quaternion.AngleAxis(180,Vector3.forward);
            }
        }
        
        if (wallType.Length == 2)
        {
            if (wallType.Contains("RT"))
            {
                return Quaternion.AngleAxis(-90,Vector3.forward);
            }
            if (wallType.Contains("TL"))
            {
                return Quaternion.AngleAxis(180,Vector3.forward);
            }
            if (wallType.Contains("BL"))
            {
                col = _mapTextureData.WallPerspective2Col;
                return Quaternion.AngleAxis(180,Vector3.up);
            }
            col = _mapTextureData.WallPerspective2Col;
        }
        
        
        if (wallType.Length == 1)
        {
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
        }
        
        return Quaternion.identity;
    }

    private void InstantiateWall(string wallType, int x, int y)
    {
        Transform wall = Instantiate(_wall,new Vector3(y, x), FindRotationObject(wallType, out Vector2[] col), transform);
        SpriteRenderer wallSpriteRenderer = wall.GetComponent<SpriteRenderer>();
        EdgeCollider2D edgeCollider2D = wall.GetComponent<EdgeCollider2D>();
        edgeCollider2D.points = col;
        wallSpriteRenderer.sprite = FindWallTexture(wallType);
    }
}
