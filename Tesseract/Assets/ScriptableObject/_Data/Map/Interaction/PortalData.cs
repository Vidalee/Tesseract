using UnityEngine;

[CreateAssetMenu(fileName = "Portal", menuName = "Map/Portal")]
public class PortalData : ScriptableObject
{
    [SerializeField] protected Sprite _sprite;
    [SerializeField] protected Vector2[] col;
    [SerializeField] protected Vector2[] persCol;

    public Sprite Sprite => _sprite;

    public Vector2[] Col => col;

    public Vector2[] PersCol => persCol;
}
