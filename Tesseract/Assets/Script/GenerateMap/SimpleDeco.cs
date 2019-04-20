using UnityEngine;

public class SimpleDeco : MonoBehaviour
{
    private SimpleDecoration _simpleDecoration;
    private Animator _a;

    public void Create(SimpleDecoration simpleDecoration)
    {
        _simpleDecoration = simpleDecoration;
        _a = GetComponent<Animator>();

        Initialise();
    }

    public void Initialise()
    {
        AnimatorOverrideController aoc = new AnimatorOverrideController(_a.runtimeAnimatorController);
        AnimatorOverride.AnimationOverride("Default", _simpleDecoration.Anim, aoc, _a);
        
        if (_simpleDecoration.AsCol)
        {
            gameObject.AddComponent<EdgeCollider2D>().points = _simpleDecoration.Col;
        }

        SpriteRenderer s = GetComponent<SpriteRenderer>();
        s.sortingOrder = _simpleDecoration.SortingOrder;
        s.material = _simpleDecoration.Material;
        
        Light o = GetComponentInChildren<Light>();
        o.color = new Color(_simpleDecoration.Color[0], _simpleDecoration.Color[1], _simpleDecoration.Color[2]);
        o.intensity = _simpleDecoration.Intensity;
        o.range = _simpleDecoration.Range;
    }
}
