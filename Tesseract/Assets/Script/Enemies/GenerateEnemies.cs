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
    private EnemyData[,] _grid;
    public static List<RoomData> RoomData;
    public static bool[,] availablePosGrid;
    
    [SerializeField] protected List<EnemyData> Enemies;
    [SerializeField] protected List<Weapons> weapons;
    private void Start()
    {
        List<PlayerData> playerDatas = new List<PlayerData>();
        foreach (Transform player in players)
        {
            Debug.Log(player);
            playerDatas.Add(player.parent.GetComponent<PlayerManager>().PlayerData);
        }
        
        _grid = new EnemyData[MapHeight, MapWidth];
        int weaponsNbr = weapons.Count;
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
                    EnemyData newEnemy = ScriptableObject.CreateInstance<EnemyData>();
                    newEnemy.Create(Enemies[Random.Range(0, 2)], x, y);
                    
                    enemy.GetComponent<Attack>().Create(newEnemy, players[0]);
                    enemy.GetComponent<EnemiesLive>().Create(newEnemy, weapons[Random.Range(0, weaponsNbr)]); 
                    enemy.GetComponent<EnemiesMovement>().Create(newEnemy, players, playerDatas, BlockingLayer);
                    enemy.GetComponent<Pathfinding>().Create(newEnemy);
                    enemy.GetComponentInChildren<SpriteRenderer>().sprite = newEnemy.Sprite;

                    enemiesNumber--;
                }
            }
        }
    }
}
