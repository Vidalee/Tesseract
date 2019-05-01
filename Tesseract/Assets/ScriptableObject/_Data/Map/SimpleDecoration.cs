using System.Diagnostics.SymbolStore;
using UnityEngine;

[CreateAssetMenu(fileName = "Decoration", menuName = "Map/Decoration")]
public class SimpleDecoration : ScriptableObject
{
    [SerializeField] protected AnimationClip anim;
    [SerializeField] protected Vector2[] col;
    [SerializeField] protected bool asCol;
    [SerializeField] protected int[] color;
    [SerializeField] protected int range;
    [SerializeField] protected float intensity;
    [SerializeField] protected Material material;
    [SerializeField] protected bool rot;
    [SerializeField] protected bool behind;

    public AnimationClip Anim => anim;

    public Vector2[] Col => col;

    public bool AsCol => asCol;

    public int[] Color => color;

    public int Range => range;

    public float Intensity => intensity;

    public bool Rot => rot;

    public Material Material => material;

    public bool Behind => behind;
}
