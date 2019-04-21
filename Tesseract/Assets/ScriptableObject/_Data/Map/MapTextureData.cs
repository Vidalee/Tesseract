using UnityEngine;

[CreateAssetMenu(fileName = "Wall", menuName = "Map/Room/Textures")]
public class MapTextureData : ScriptableObject
{
    #region Variable

    [SerializeField] protected Sprite[] floor;
    
    [SerializeField] protected Sprite[] wall;
    
    [SerializeField] protected Sprite wall1Side;

    [SerializeField] protected Sprite wall2Side;

    [SerializeField] protected Sprite wall3Side;
    
    [SerializeField] protected Sprite wall4Side;

    [SerializeField] protected Sprite wallCorner;

    [SerializeField] protected Sprite wallDoubleSide;
    
    [SerializeField] protected Vector2[] wallPerspective1Col =
    {
        new Vector2(-0.5f,0), new Vector2(0.5f, 0),
    };
    
    [SerializeField] protected Vector2[] wallPerspective2Col =
    {
        new Vector2(-0.5f,0), new Vector2(0.5f, 0), new Vector2(0.5f, -0.5f), 
    };
    
    [SerializeField] protected Vector2[] cubeCol =
    {
        new Vector2(-0.5f, -0.5f), new Vector2(-0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, -0.5f), new Vector2(-0.5f, -0.5f)
    };

    [SerializeField] protected Vector2[] cornerCol =
    {
        new Vector2(-0.5f, 0.5f), new Vector2(0.5f,0.5f), new Vector2(0.5f, -0.5f), 
    };

    [SerializeField] protected Vector2[] demiCol =
    {
        new Vector2(-0.5f,0), new Vector2(0.5f, 0), new Vector2(0.5f, -0.5f), new Vector2(-0.5f, 0.5f), new Vector2(-0.5f, 0),  
    };

    [SerializeField] protected Vector2[] demiCol2 =
    {
        new Vector2(-0.5f, 0), new Vector2(-0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0), new Vector2(-0.5f, 0)
    };

    #endregion

    #region Set/Get

    public Sprite[] Floor => floor;

    public Sprite[] Wall => wall;

    public Sprite Wall1Side => wall1Side;

    public Sprite Wall2Side => wall2Side;

    public Sprite Wall3Side => wall3Side;

    public Sprite Wall4Side => wall4Side;

    public Sprite WallCorner => wallCorner;

    public Sprite WallDoubleSide => wallDoubleSide;

    public Vector2[] WallPerspective1Col => wallPerspective1Col;

    public Vector2[] WallPerspective2Col => wallPerspective2Col;

    public Vector2[] CubeCol => cubeCol;

    public Vector2[] CornerCol => cornerCol;

    public Vector2[] DemiCol => demiCol;

    public Vector2[] DemiCol2 => demiCol2;

    #endregion
}