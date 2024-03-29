﻿using System;
using Script.GlobalsScript;
using Script.GlobalsScript.Struct;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectiles : MonoBehaviour
{
    #region Variable

    private Animator _a;
    private ProjectilesData _projectilesData;
    public Transform projectilesAnimation;
    public GameEvent Damage;

    #endregion

    #region Initialise

    public void Create(ProjectilesData data)
    {
        _projectilesData = data;
        Transform o = Instantiate(projectilesAnimation, transform.position, Quaternion.identity, transform);
        o.GetComponent<ProjectilesAnimation>().Create(_projectilesData);
    }

    #endregion

    #region Update

    private void FixedUpdate()
    {
        Translate();
    }

    #endregion

    #region Collision

    private void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.gameObject.tag;
        
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("ObstaclesDestroy"))
        {
            Destroy(gameObject);
        }
        
        if (tag == _projectilesData.EnemyTag && tag == "Enemies")
        {
            if(--_projectilesData.Live <= 0) Destroy(gameObject);
            EnemiesLive e = other.transform.GetComponent<EnemiesLive>();
            
            e.GetDamaged(_projectilesData.DamageP, _projectilesData.DamageM);
            if (Random.Range(0, 100) < _projectilesData.Prob) e.Effect(_projectilesData.Effect, _projectilesData.EffectDamage, _projectilesData.Duration);
        }
        
        else if (tag == _projectilesData.BossTag && tag == "Boss")
        {
            if(--_projectilesData.Live <= 0) Destroy(gameObject);
            BossLive e = other.transform.GetComponent<BossLive>();
            
            e.GetDamaged(_projectilesData.DamageP, _projectilesData.DamageM);
        }
        
        else if (tag == "Player" && tag == _projectilesData.EnemyTag)
        {
            Damage.Raise(new EventArgsInt(_projectilesData.DamageP + _projectilesData.DamageM));
            Destroy(gameObject);
        }
    }

    #endregion

    #region Movement

    public void Translate()
    {
        transform.Translate(_projectilesData.Direction * Time.deltaTime * _projectilesData.Speed);
    }

    #endregion
}