using UnityEngine;

[CreateAssetMenu(fileName = "Wall", menuName = "Map/Room/Textures")]
public class MapTextureData : ScriptableObject
{
    [SerializeField] protected Sprite[] floor;
    
    [SerializeField] protected Sprite[] wall;
    
    [SerializeField] protected Sprite wall1Side;
    [SerializeField] protected Sprite wall2Side;
    [SerializeField] protected Sprite wall3Side;

    [SerializeField] protected Sprite wallCorner;

    [SerializeField] protected Sprite wallDoubleSide;



    public Sprite[] Floor => floor;

    public Sprite[] Wall => wall;

    public Sprite Wall1Side => wall1Side;

    public Sprite Wall2Side => wall2Side;

    public Sprite Wall3Side => wall3Side;

    public Sprite WallCorner => wallCorner;

    public Sprite WallDoubleSide => wallDoubleSide;
}