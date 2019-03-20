using UnityEngine;

[CreateAssetMenu(fileName = "Wall", menuName = "Map/Room/Textures")]
public class MapTextureData : ScriptableObject
{
    [SerializeField] protected Sprite[] floor;
    
    [SerializeField] protected Sprite[] wall;
    
    [SerializeField] protected Sprite wallSide;
    [SerializeField] protected Sprite wallCorner;

    public Sprite[] Floor => floor;

    public Sprite[] Wall => wall;

    public Sprite WallSide => wallSide;

    public Sprite WallCorner => wallCorner;
}