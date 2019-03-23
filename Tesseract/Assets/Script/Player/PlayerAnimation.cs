using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] protected AnimatorOverride Ao;
    [SerializeField] protected AnimatorOverrideController Aoc;
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
        Ao.AnimationOverride("DefaultMove", PlayerData.Move, Aoc, _a);
        Ao.AnimationOverride("DefaultIdle", PlayerData.Idle, Aoc, _a);
        Ao.AnimationOverride("DefaultThrow", PlayerData.Throw, Aoc, _a);
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

    public void Shuriken1()
    {
        StartCoroutine(Shuriken1A());
    }
    
    IEnumerator Shuriken1A()
    {       
        _a.SetBool("OtherAction", true);
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        bool dir = Math.Abs(diff.x) > Math.Abs(diff.y);

        if (dir)
        {
            _a.Play(diff.x < 0 ? "DefaultThrowL" : "DefaultThrowR");
        }
        else
        {
            _a.Play(diff.y < 0 ? "DefaultThrowB" : "DefaultThrowT");
        }

        yield return new WaitForSeconds(0.2f);
        _a.SetBool("OtherAction", false);
    }
}
