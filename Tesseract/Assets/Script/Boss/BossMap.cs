using UnityEngine;
using UnityEngine.Tilemaps;

public class BossMap : MonoBehaviour
{
    private bool[,] _map;
    public Transform WallTexture;
    public MapTextureData MapTextureData;
    
    public Tilemap FloorMap;
    public Tilemap PerspMap;
    public Tilemap WallMap;
    public Tilemap ShadWMap;
    public Tilemap[] ShadSMap;
    public Tilemap[] ShadCornMap;

    private void Start()
    {
        _map = new bool[25,25];
        WallMap.GetComponent<Renderer>().sortingOrder = 30 * -105;

        PlaceFloor();
        CreateFloor();
        Instantiate(WallTexture).GetComponent<GenerateWall>().Create(_map, PerspMap, WallMap, ShadWMap, ShadSMap, ShadCornMap);
    }

    private void PlaceFloor()
    {
        for (int i = 2; i < 20; i++)
        {
            for (int j = 2; j < 20; j++)
            {
                _map[i, j] = true;
            }
        }
    }
    
    private void CreateFloor()
    {
        FloorMap.GetComponent<Renderer>().sortingOrder = 30 * -105;
        
        Tile tile = ScriptableObject.CreateInstance<Tile>();

        int len = MapTextureData.Floor.Length;
        for (int i = 0; i < _map.GetLength(0); i++)
        {
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                if (_map[i, j])
                {
                    tile.sprite = MapTextureData.Floor[Random.Range(0, len)];
                    FloorMap.SetTile(new Vector3Int(j, i, 0), tile);
                }
            }
        }
    }
}
