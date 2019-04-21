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

        if (_simpleDecoration.Color.Length != 0)
        {
            GameObject light = Instantiate(new GameObject("Light"), transform.position - new Vector3(0, 0, 0.5f), Quaternion.identity, transform);
            Light o = light.gameObject.AddComponent<Light>();
            
            o.color = new Color(_simpleDecoration.Color[0], _simpleDecoration.Color[1], _simpleDecoration.Color[2]);
            o.intensity = _simpleDecoration.Intensity;
            o.range = _simpleDecoration.Range;
        }
    }
}
