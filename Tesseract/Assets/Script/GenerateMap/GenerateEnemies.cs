using System.Collections.Generic;
using Script.Enemies;
using Script.Pathfinding;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public int MapHeight;
    public int MapWidth;

    public int seed;
    
    public static List<Transform> players = new List<Transform>();
    public LayerMask BlockingLayer;

    
    [SerializeField] protected GameObject Enemy;
    private Enemy[,] _grid;
    public static List<RoomData> RoomData;
    public static bool[,] availablePosGrid;
    
    [SerializeField] protected List<Enemy> Enemies;

    private void Start()
    {
        //players.Add(PlayerManager.Player);
        _grid = new Enemy[MapHeight, MapWidth];
        Random.InitState(seed); 
        
        foreach (RoomData roomData in RoomData)
        {
            //int roomSpace = roomData.Width * roomData.Height;
            int enemiesNumber = 2;
            while (enemiesNumber != 0)
            {
                int x = roomData.X1 + Random.Range(0, roomData.Width);
                int y = roomData.Y1 + Random.Range(0, roomData.Height);
                if (availablePosGrid[y + 1, x] && availablePosGrid[y, x] && _grid[y, x] == null)
                {
                    GameObject enemy = Instantiate(Enemy, new Vector3(x, y, 0), Quaternion.identity);
                    Enemy newEnemy = ScriptableObject.CreateInstance<Enemy>();
                    newEnemy.Create(Enemies[Random.Range(0, 1)], x, y);
                    
                    
                    enemy.GetComponent<Attack>().Create(newEnemy, players[0]);
                    enemy.GetComponent<EnemiesLive>().Create(newEnemy); 
                    enemy.GetComponent<EnemiesMovement>().Create(newEnemy, players, BlockingLayer);
                    enemy.GetComponent<Pathfinding>().Create(newEnemy);
                    enemy.GetComponentInChildren<SpriteRenderer>().sprite = newEnemy.Sprite;

                    enemiesNumber--;
                }
            }
        }
    }
}
