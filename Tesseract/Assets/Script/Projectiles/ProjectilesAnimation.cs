using UnityEngine;

public class ProjectilesAnimation : MonoBehaviour
{
    #region Variable

    private ProjectilesData _projectilesData;

    #endregion

    #region Initialise

    public void Create(ProjectilesData projectilesData)
    {
        _projectilesData = projectilesData;
        Animation();
    }

    #endregion

    #region Update

    private void Update()
    {
        Rotation();
        LightColor();
    }

    #endregion

    #region Animation

    private void Animation()
    {
        Debug.Log("test");
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

    #endregion

    #region Movement

    private void Rotation()
    {
        Vector2 dir = _projectilesData.Direction;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    #endregion
}
