using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Script.Enemies;
using Script.GlobalsScript;
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
    public List<GameObject> enemiesList;

    [SerializeField] protected List<EnemyData> Enemies;
    private void Start()
    {
        List<PlayerData> playerDatas = new List<PlayerData>();
        foreach (Transform player in players)
        {
            playerDatas.Add(player.parent.GetComponent<PlayerManager>().PlayerData);
        }
        
        _grid = new EnemyData[MapHeight, MapWidth];
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
                    EnemyData enemyData = Enemies[Random.Range(0, Enemies.Count)];
                    
                    if (enemyData.Name == "Bat")
                    {
                        CreateEnemy(enemyData, x, y, playerDatas);
                        CreateEnemy(enemyData, x + 0.25f, y + 0.1f, playerDatas);
                        CreateEnemy(enemyData, x - 0.1f, y - 0.25f, playerDatas);
                        CreateEnemy(enemyData, x - 0.25f, y + 0.1f, playerDatas);
                        CreateEnemy(enemyData, x + 0.1f, y + 0.25f, playerDatas);
                    }
                    else if (enemyData.Name == "Spider")
                    {
                        _grid[y, x] = enemyData;
                        CreateEnemy(enemyData, x, y, playerDatas);
                        
                        bool found = false;
                        while (!found)
                        {
                            int x1 = roomData.X1 + Random.Range(0, roomData.Width);
                            int y1 = roomData.Y1 + Random.Range(0, roomData.Height);
                            if (availablePosGrid[y1 + 1, x1] && availablePosGrid[y1, x1] && _grid[y1, x1] == null)
                            {
                                CreateEnemy(enemyData, x1, y1, playerDatas);
                                _grid[x1, y1] = enemyData;
                                found = true;
                            }
                        }
                    }
                    else
                    {
                        
                        CreateEnemy(enemyData, x, y, playerDatas);
                    }
                    
                    _grid[y, x] = enemyData;
                    enemiesNumber--;
                }
            }
        }
    }

    private void CreateEnemy(EnemyData enemyData, float x, float y, List<PlayerData> playerDatas)
    {
        EnemyData newEnemy = ScriptableObject.CreateInstance<EnemyData>();
        newEnemy.Create(enemyData, x, y, StaticData.RandomLevel());
        
        GameObject enemy = Instantiate(Enemy, new Vector3(x, y, 0), Quaternion.identity);
        enemiesList.Add(enemy);            
        
        Animator animator = enemy.GetComponentInChildren<Animator>();
        SetAnimation(newEnemy, animator);
                            
        enemy.GetComponent<Attack>().Create(newEnemy, players, animator);
        enemy.GetComponent<EnemiesLive>().Create(newEnemy, playerDatas); 
        enemy.GetComponent<EnemiesMovement>().Create(newEnemy, players, playerDatas, BlockingLayer, animator);
        enemy.GetComponent<Pathfinding>().Create(newEnemy);
        enemy.GetComponentInChildren<SpriteRenderer>().sprite = newEnemy.Sprite;
        enemy.GetComponent<BoxCollider2D>().size = new Vector3(newEnemy.ColliderX, newEnemy.ColliderY);
        enemy.transform.GetChild(2).transform.position = new Vector3(x, y, 0) + new Vector3(0, newEnemy.EffectY + 0.181f, 0);
        enemy.transform.GetChild(3).transform.position = new Vector3(x, y, 0) + new Vector3(-0.44f, newEnemy.EffectY, 0);
        enemy.transform.GetChild(4).transform.position = new Vector3(x, y, 0) + new Vector3(0, newEnemy.EffectY, 0);
    }
    
    private void SetAnimation(EnemyData enemyData, Animator animator)
    {
        AnimatorOverrideController aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);
        AnimatorOverride.EnemyAnimationOverride("Walk", enemyData.Move, aoc, animator);
        AnimatorOverride.EnemyAnimationOverride("Idle", enemyData.Idle, aoc, animator);
        AnimatorOverride.EnemyAnimationOverride("Attack", enemyData.Attack, aoc, animator);
    }
}
