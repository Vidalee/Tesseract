using UnityEngine;
using UnityEngine.WSA;

[CreateAssetMenu(fileName = "Wall", menuName = "Map/Room/Textures")]
public class MapTextureData : ScriptableObject
{
    #region Variable

    [SerializeField] protected Sprite[] floor;
    
    [SerializeField] protected Sprite[] wall;
    [SerializeField] protected Sprite[] wall1Side;
    [SerializeField] protected Sprite[] wall2Side;
    [SerializeField] protected Sprite[] wall3Side;
    
    [SerializeField] protected Sprite wall4Side;
    [SerializeField] protected Sprite[] wallCorner;
    [SerializeField] protected Sprite[] wallDoubleSide;

    [SerializeField] protected Sprite[] shadowSide;
    [SerializeField] protected Sprite[] shadowCorner;
    [SerializeField] protected Sprite shadowWall;
    
    [SerializeField] protected Sprite[] miniMap;

    #endregion

    #region Set/Get

    public Sprite[] Floor => floor;

    public Sprite[] Wall => wall;

    public Sprite[] Wall1Side => wall1Side;

    public Sprite[] Wall2Side => wall2Side;

    public Sprite[] Wall3Side => wall3Side;

    public Sprite Wall4Side => wall4Side;

    public Sprite[] WallCorner => wallCorner;

    public Sprite[] WallDoubleSide => wallDoubleSide;

    public Sprite[] ShadowSide => shadowSide;

    public Sprite[] ShadowCorner => shadowCorner;

    public Sprite ShadowWall => shadowWall;

    public Sprite[] MiniMap => miniMap;

    #endregion
}