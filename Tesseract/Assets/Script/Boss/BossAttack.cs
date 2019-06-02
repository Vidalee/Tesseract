using System.Collections;
using System.Collections.Generic;
using Script.Enemies;
using Script.GlobalsScript;
using Script.GlobalsScript.Struct;
using Script.Pathfinding;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] public GameEvent PlayerDamage;
    
    [SerializeField] protected Animator _a;
    [SerializeField] protected SpriteRenderer _sprite;
    
    [SerializeField] protected bool waitingCooldown = false;
    [SerializeField] protected float _cooldown;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _attackRange;

    [SerializeField] protected GameObject _enemy;
    [SerializeField] protected EnemyData batData;
    
    private Transform _player;
    [SerializeField] protected PlayerData _playerData;
    [SerializeField] protected List<Transform> _players = new List<Transform>();
    [SerializeField] protected List<PlayerData> _playerDatas = new List<PlayerData>();
    
    [SerializeField] protected LayerMask _blockingLayer;

    public void TryAttack(string attack)
    {
        if (attack == "AttackBat")
        {
            _a.Play("AttackBat");
            GenerateBats();
        }
        else
        {
            _a.Play("AttackAxe");
            PlayerDamage.Raise(new EventArgsInt(_damage));
        }
    }
  
    IEnumerator Cooldown()
    {
        waitingCooldown = true;
        yield return new WaitForSeconds(_cooldown);
        waitingCooldown = false;
    }

    private void Update()
    {
        if (_player == null)
        {
            _player = GameObject.Find("Player(Clone)").transform;
            if(_player == null) return;
            _playerData = _player.GetComponent<PlayerMovement>()._playerData;
            _players.Add(_player);
            _playerDatas.Add(_playerData);
            return;
        }
        
        _sprite.sortingOrder = (int) (transform.position.y * -10);
        if (!waitingCooldown)
        {
            if ((_player.position - transform.position).sqrMagnitude < _attackRange * _attackRange + 0.2f)
            {
                TryAttack("AttackAxe");
            }
            else
            {
                TryAttack("AttackBat");
            }

            StartCoroutine(Cooldown());
        }
    }

    private void GenerateBats()
    {
        int batsNbr = Random.Range(5, 10);
        float rot = 90 / (float) batsNbr;

        for (int i = 0; i < batsNbr; i += 2)
        {
            Vector3 dir = (_player.position - transform.position).normalized * 2;
            Vector3 pos1 = transform.position + Quaternion.Euler(0, 0, rot * i) * dir;
            Vector3 pos2 = transform.position + Quaternion.Euler(0, 0, rot * - i) * dir;
            if (pos1.x > 2 && pos1.x < 19 && pos1.y > 2)
            {
                GenerateBat(pos1.x, pos1.y);
            }

            if (pos2.x > 2 && pos2.x < 19 && pos2.y > 2)
            {
                GenerateBat(pos2.x, pos2.y);
            }
        }
    }

    private void GenerateBat(float x, float y)
    {
        EnemyData newEnemy = ScriptableObject.CreateInstance<EnemyData>();
        newEnemy.Create(batData, transform.position.x, transform.position.y, StaticData.RandomLevel());
        
        GameObject enemy = Instantiate(_enemy, new Vector3(x, y, 0), Quaternion.identity);

        Animator animator = enemy.GetComponentInChildren<Animator>();
        SetAnimation(newEnemy, animator);
                
        enemy.GetComponent<Attack>().Create(newEnemy, _players, animator);
        enemy.GetComponent<EnemiesLive>().Create(newEnemy, _playerDatas); 
        enemy.GetComponent<EnemiesMovement>().Create(newEnemy, _players, _playerDatas, _blockingLayer, animator);
        enemy.GetComponent<Pathfinding>().Create(newEnemy);
        enemy.GetComponentInChildren<SpriteRenderer>().sprite = newEnemy.Sprite;
        enemy.GetComponent<BoxCollider2D>().size = new Vector3(newEnemy.ColliderX, newEnemy.ColliderY);
        enemy.transform.GetChild(0).GetComponent<BoxCollider2D>().size = new Vector3(newEnemy.ColliderX, newEnemy.ColliderY);
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
