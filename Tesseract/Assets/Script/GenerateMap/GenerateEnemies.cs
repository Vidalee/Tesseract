using System.Collections.Generic;
using Script.Enemies;
using Script.Pathfinding;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public int MapHeight;
    public int MapWidth;

    public int seed;
    
    [SerializeField] protected List<GameObject> Players;
    public LayerMask BlockingLayer;

    
    [SerializeField] protected GameObject Enemy;
    private Enemy[,] _grid;
    public static List<RoomData> RoomData;
    public static bool[,] availablePosGrid;
    
    [SerializeField] protected List<Enemy> Enemies;

    private void Start()
    {
        _grid = new Enemy[MapHeight, MapWidth];
        Random.InitState(seed); 
        
        foreach (RoomData roomData in RoomData)
        {
            int roomSpace = roomData.Width * roomData.Height;
            int enemiesNumber = roomSpace / 20;
            while (enemiesNumber != 0)
            {
                int x = roomData.X1 + Random.Range(0, roomData.Width);
                int y = roomData.Y1 + Random.Range(0, roomData.Height);
                if (_grid[y, x] == null)
                {
                    GameObject enemy = Instantiate(Enemy, new Vector3(x, y, 0), Quaternion.identity);
                    Enemy newEnemy = ScriptableObject.CreateInstance<Enemy>();
                    newEnemy.Create(Enemies[Random.Range(0, 2)], x, y);
                    
                    
                    enemy.GetComponent<Attack>().Create(newEnemy);
                    enemy.GetComponent<EnemiesLive>().Create(newEnemy); 
                    enemy.GetComponent<EnemiesMovement>().Create(newEnemy, Players, BlockingLayer);
                    enemy.GetComponent<Pathfinding>().Create(newEnemy);
                    enemy.GetComponentInChildren<SpriteRenderer>().sprite = newEnemy.Sprite;

                    enemiesNumber--;
                }
            }
        }
    }
}
