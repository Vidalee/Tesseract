using UnityEngine;

public class ProjectilesAnimation : MonoBehaviour
{
    [SerializeField] protected AnimatorOverrideController Aoc;
    [SerializeField] protected AnimatorOverride Ao;
    private ProjectilesData _projectilesData;
    private Animator _a;


    public void Create(ProjectilesData projectilesData)
    {
        _a = GetComponent<Animator>();
        _projectilesData = projectilesData;
        Animation();
    }

    private void Update()
    {
        Rotation();
    }

    public void Animation()
    {
        Ao.AnimationOverride("DefaultProjectiles", _projectilesData.Anim, Aoc, _a);
    }

    public void Rotation()
    {
        Vector2 dir = _projectilesData.Direction;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
