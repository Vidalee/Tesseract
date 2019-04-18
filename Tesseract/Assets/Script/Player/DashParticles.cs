using System;
using UnityEngine;

public class DashParticles : MonoBehaviour
{
    public PlayerData _playerData;
    private Animator _a;

    private void Awake()
    {
        _a = GetComponent<Animator>();
    }

    public void Create(PlayerData playerData)
    {
        _playerData = playerData;
        SetAnimation();
    }
    
    private void SetAnimation()
    {
        AnimatorOverrideController aoc = new AnimatorOverrideController(_a.runtimeAnimatorController);
        AnimatorOverride.AnimationOverride("DefaultDashParticules", _playerData.DashParticles, aoc, _a);
    }

    public void PlayerDashParticles()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        bool dir = Math.Abs(diff.x) > Math.Abs(diff.y);

        _a.Play("DefaultDashParticules");
    }
}
