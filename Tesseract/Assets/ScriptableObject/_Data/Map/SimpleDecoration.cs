using UnityEngine;

[CreateAssetMenu(fileName = "Decoration", menuName = "Map/Decoration")]
public class SimpleDecoration : ScriptableObject
{
    [SerializeField] protected Sprite sprite;
    [SerializeField] protected Vector2[] col;
    [SerializeField] protected bool asCol;

    public Sprite Sprite => sprite;

    public Vector2[] Col => col;

    public bool AsCol => asCol;
}
