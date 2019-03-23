using UnityEngine;

public class ProjectilesAnimation : MonoBehaviour
{
    [SerializeField] protected AnimatorOverrideController Aoc;
    [SerializeField] protected AnimatorOverride Ao;
    private Projectiles _projectiles;
    private Animator _a;


    private void Start()
    {
        GetComponent();
        Animation();
    }

    private void GetComponent()
    {
        _a = GetComponent<Animator>();
        _projectiles = transform.parent.GetComponent<Projectiles>();
    }

    public void Animation()
    {
        Ao.AnimationOverride("DefaultProjectiles", _projectiles.ProjectilesData.Anim, Aoc, _a);
    }
}
