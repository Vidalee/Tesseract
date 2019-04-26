using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    #region Variable

    public PlayerData _playerData;
    private Animator _a;
    private SpriteRenderer _spriteRenderer;

    #endregion

    #region Initialise

    private void Update()
    {
        _spriteRenderer.sortingOrder = (int) (transform.position.y * -100);
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _a = GetComponent<Animator>();
    }

    public void Create(PlayerData playerData)
    {
        _playerData = playerData;
        //transform.GetComponentInChildren<DashParticles>().Create(_playerData);
        SetAnimation();
    }
    
    private void SetAnimation()
    {
        AnimatorOverrideController aoc = new AnimatorOverrideController(_a.runtimeAnimatorController);
        AnimatorOverride.AnimationOverride("DefaultMove", _playerData.Move, aoc, _a);
        AnimatorOverride.AnimationOverride("DefaultIdle", _playerData.Idle, aoc, _a);
        AnimatorOverride.AnimationOverride("DefaultAttack", _playerData.Attack, aoc, _a);
        AnimatorOverride.AnimationOverride("DefaultDash", _playerData.Dash, aoc, _a);
    }

    #endregion

    #region Animation

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

        if (_playerData.Name == "Mage") _a.speed = 0.01f;
        
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

    #endregion
}
