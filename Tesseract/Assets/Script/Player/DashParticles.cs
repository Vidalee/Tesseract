using System;
using UnityEngine;

public class DashParticles : MonoBehaviour
{
    #region Variable

    public PlayerData _playerData;
    private Animator _a;

    #endregion

    #region Initialise

    private void Awake()
    {
        _a = GetComponent<Animator>();
    }

    public void Create(PlayerData playerData)
    {
        _playerData = playerData;
        SetAnimation();
    }

    #endregion

    #region Animation

    private void SetAnimation()
    {
        AnimatorOverrideController aoc = new AnimatorOverrideController(_a.runtimeAnimatorController);
        AnimatorOverride.AnimationOverride("DefaultDashParticules", _playerData.DashParticles, aoc, _a);
    }

    public void PlayerDashParticles()
    {
        _a.Play("DefaultDashParticules");
    }

    #endregion
}
