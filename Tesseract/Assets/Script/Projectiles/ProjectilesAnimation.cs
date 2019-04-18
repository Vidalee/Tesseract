using UnityEngine;

public class ProjectilesAnimation : MonoBehaviour
{
    private ProjectilesData _projectilesData;


    public void Create(ProjectilesData projectilesData)
    {
        _projectilesData = projectilesData;
        Animation();
    }

    private void Update()
    {
        Rotation();
        LightColor();
    }

    private void Animation()
    {
        Animator animator = GetComponent<Animator>();
        AnimatorOverrideController aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);
        AnimatorOverride.AnimationOverride("DefaultProjectiles", _projectilesData.Anim, aoc, animator);
    }

    private void LightColor()
    {
        Light light = GetComponentInChildren<Light>();
        int[] col = _projectilesData.Color;
        light.color = new Color(col[0], col[1], col[2]);
    }

    private void Rotation()
    {
        Vector2 dir = _projectilesData.Direction;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
