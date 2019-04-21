using UnityEngine;

[CreateAssetMenu(fileName = "Decoration", menuName = "Map/Decoration")]
public class SimpleDecoration : ScriptableObject
{
    [SerializeField] protected AnimationClip anim;
    [SerializeField] protected Vector2[] col;
    [SerializeField] protected bool asCol;
    [SerializeField] protected int[] color;
    [SerializeField] protected string name;
    [SerializeField] protected int range;
    [SerializeField] protected float intensity;
    [SerializeField] protected int sortingOrder;
    [SerializeField] protected Material material;

    [SerializeField] protected Transform interaction;

    private void OnEnable()
    {
        if(sortingOrder == 0) sortingOrder = 1;
    }

    public AnimationClip Anim => anim;

    public Vector2[] Col => col;

    public bool AsCol => asCol;

    public int[] Color => color;

    public string Name => name;

    public int Range => range;

    public float Intensity => intensity;

    public int SortingOrder => sortingOrder;

    public Material Material => material;
}
