using System;
using System.Collections;
using System.Collections.Generic;
using Script.GlobalsScript;
using Script.GlobalsScript.Struct;
using UnityEngine;

public class EnemiesAnimation : MonoBehaviour
{
    public EnemyData enemyData;
    private Animator _a;
    
    public void Create(EnemyData enemyData, Animator animator)
    {
        this.enemyData = enemyData;
        _a = animator;
    }
    


    public void EnemyMovingAnimation(IEventArgs args)
    {
        EventArgsCoor coor = args as EventArgsCoor;
        
        int speed = coor.X != 0 ? coor.X : coor.Y;
        bool dir = coor.X == 0;
        
        if (speed == 0)
        {
            _a.SetInteger("Speed", 0);
            return;
        }
                
        _a.SetInteger("Speed",speed);
        _a.SetBool("Direction",dir);
    }
    
    public void EnemyAttackAnimation()
    {
        StartCoroutine(PlayerAttackCoroutine());
    }
    
    IEnumerator PlayerAttackCoroutine()
    {       
        _a.SetBool("OtherAction", true);
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        bool dir = Math.Abs(diff.x) > Math.Abs(diff.y);
        
        if (enemyData.Name == "Warrior") _a.speed = 10f;

        if (dir)
        {
            _a.Play(diff.x < 0 ? "DefaultAttackL" : "DefaultAttackR");
        }
        else
        {
            _a.Play(diff.y < 0 ? "DefaultAttackB" : "DefaultAttackT");
        }

        yield return new WaitForSeconds(0.2f);
        _a.SetBool("OtherAction", false);
        _a.speed = 1;
    }
}
