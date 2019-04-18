using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] protected PlayerData PlayerData;

    private Animator _a;

    private void Start()
    {
        GetComponent();
        SetAnimation();
    }

    private void GetComponent()
    {
        _a = GetComponent<Animator>();
    }

    private void SetAnimation()
    {
        AnimatorOverrideController aoc = new AnimatorOverrideController(_a.runtimeAnimatorController);
        AnimatorOverride.AnimationOverride("DefaultMove", PlayerData.Move, aoc, _a);
        AnimatorOverride.AnimationOverride("DefaultIdle", PlayerData.Idle, aoc, _a);
        AnimatorOverride.AnimationOverride("DefaultAttack", PlayerData.Attack, aoc, _a);
        AnimatorOverride.AnimationOverride("DefaultDash", PlayerData.Dash, aoc, _a);
    }
    
    public void PlayerMovingAnimation(IEventArgs args)
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

    public void PlayerDashAnimation()
    {
        StartCoroutine(PlayerDashCor());
    }
    
    IEnumerator PlayerDashCor()
    {
        _a.SetBool("OtherAction", true);
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        bool dir = Math.Abs(diff.x) > Math.Abs(diff.y);

        if (PlayerData.Name == "Mage") _a.speed = 0.01f;
        
        if (dir)
        {
            _a.Play(diff.x < 0 ? "DefaultDashL" : "DefaultDashR");
        }
        else
        {
            _a.Play(diff.y < 0 ? "DefaultDashB" : "DefaultDashT");
        }

        yield return new WaitForSeconds(0.3f);
        _a.SetBool("OtherAction", false);
        _a.speed = 1;
    }

    public void PlayerAttackAnimation()
    {
        StartCoroutine(PlayerAttackCoroutine());
    }
    
    IEnumerator PlayerAttackCoroutine()
    {       
        _a.SetBool("OtherAction", true);
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        bool dir = Math.Abs(diff.x) > Math.Abs(diff.y);

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
    }
}
