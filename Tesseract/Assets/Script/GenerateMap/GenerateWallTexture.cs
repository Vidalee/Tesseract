using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateWallTexture : MonoBehaviour
{

    private bool[,] _grid;
    public MapTextureData _mapTextureData;
    public Transform _wall;
    
    public void Create(bool[,] grid)
    {
        _grid = grid;
        
        InstantiateSimpleWall();
        ChooseWall();
    }

    private void InstantiateSimpleWall()
    {
        for (int i = _grid.GetLength(1) - 2; i > 0; i--)
        {
            for (int j = _grid.GetLength(0) - 2; j > 1; j--)
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
        for (int i = _grid.GetLength(1) - 2; i > 1; i--)
        {
            for (int j = _grid.GetLength(0) - 2; j > 1; j--)
            {
                if(_grid[i, j]) continue;
                
                string wallType = "";

                if (_grid[i + 1, j]) wallType += "B";
                if (_grid[i - 1, j]) wallType += "T";
                if (_grid[i, j - 1]) wallType += "R";
                if (_grid[i, j + 1]) wallType += "L";

                if (wallType == "")
                {
                    if (_grid[i + 1, j + 1]) wallType = "CCBL";
                    if (_grid[i - 1, j - 1]) wallType = "CCTR";
                    if (_grid[i + 1, j - 1]) wallType = "CCBR";
                    if (_grid[i - 1, j + 1]) wallType = "CCTL";
                }

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
                return _mapTextureData.WallCorner;
        }

        Sprite[] wallTexture = _mapTextureData.Wall;
        return wallTexture[Random.Range(0, wallTexture.Length)];
    }

    private Quaternion FindRotationObject(string wallType)
    {
        return Quaternion.identity;
    }

    private void InstantiateWall(string wallType, int x, int y)
    {
        Transform wall = Instantiate(_wall,new Vector3(y, x), Quaternion.identity);
        SpriteRenderer wallSpriteRenderer = wall.GetComponent<SpriteRenderer>();
        wallSpriteRenderer.sprite = FindWallTexture(wallType);
    }
}
