using System;
using UnityEngine;

public class DashParticules : MonoBehaviour
{
    [SerializeField]protected PlayerData PlayerData;
    private Animator _a;

    private void Awake()
    {
        _a = GetComponent<Animator>();
        SetAnimation();
    }

    private void SetAnimation()
    {
        AnimatorOverrideController aoc = new AnimatorOverrideController(_a.runtimeAnimatorController);
        AnimatorOverride.AnimationOverride("DefaultDashParticules", PlayerData.DashParticles, aoc, _a);
    }

    public void PlayerDashParticles()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        bool dir = Math.Abs(diff.x) > Math.Abs(diff.y);

        _a.Play("DefaultDashParticules");
    }
}
