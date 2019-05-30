using System.Xml.Serialization;
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
        
        int mult = -100;
        if (_simpleDecoration.Behind)
        {
            mult = -105;
        }

        SpriteRenderer s = GetComponent<SpriteRenderer>();
        s.material = _simpleDecoration.Material;
        s.sortingOrder = (int) ((transform.parent.position.y + transform.position.y) * mult);
        if (s.sortingOrder > 10000)
        {
            Debug.Log(s.sortingOrder);

        }

        if (_simpleDecoration.Color.Length != 0)
        {
            GameObject go = new GameObject("Light");
            go.transform.parent = transform;
            GameObject light = Instantiate(go, transform.position - new Vector3(0, 0, .5f), Quaternion.identity, go.transform);
            Light o = light.gameObject.AddComponent<Light>();
            
            o.color = new Color(_simpleDecoration.Color[0], _simpleDecoration.Color[1], _simpleDecoration.Color[2]);
            o.intensity = _simpleDecoration.Intensity;
            o.range = _simpleDecoration.Range;
            o.bounceIntensity = 0;
            o.renderMode = LightRenderMode.ForcePixel;
        }
    }
}
