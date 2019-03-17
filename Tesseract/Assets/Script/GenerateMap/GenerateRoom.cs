using UnityEngine;

public class GenerateRoom : MonoBehaviour
{
    public Transform WallObj;
    public Transform FloorObj;
    
    [SerializeField] protected MapTextureData MapTexture;
    [SerializeField] protected RoomData RoomData;
    [SerializeField] protected int Height;
    [SerializeField] protected int Width;

    private Transform[,] grid;

    enum Rotation
    {
        Null,
        ClockWise,
        AntiClockWise,
        Symmetry
    };
    
    private void Awake()
    {
        RoomData.InitiateGrid(Height,Width);
        SetRoom();
    }

    private void SetRoom()
    {
        //Instantiate floor
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                Transform floor = Instantiate(FloorObj,new Vector3(i, j, 0f), Quaternion.identity, transform);
                floor.GetComponent<SpriteRenderer>().sprite = MapTexture.Floor[0];
            }
        }

        //Instantiate Left anf Right Wall
        for (int i = 1; i < Height - 1; i++)
        {
            InstantiateWall(0, i, Rotation.Null, MapTexture.WallSide);
            InstantiateWall(Width - 1, i, Rotation.Symmetry, MapTexture.WallSide);
        }
        
        //Instantiate Top Wall and Bottom Wall
        for (int i = 1; i < Width - 1; i++)
        {
            InstantiateWall(i, 0, Rotation.AntiClockWise, MapTexture.WallSide);
            InstantiateWall(i, Height - 1, Rotation.ClockWise, MapTexture.WallSide);
            InstantiateWall(i, Height - 2,Rotation.Null, MapTexture.Wall[0]);
        }
        
        //Instantiate Corner Wall
        InstantiateWall(0, 0, Rotation.AntiClockWise, MapTexture.WallCorner);
        InstantiateWall(0, Height - 1, Rotation.Null, MapTexture.WallCorner);
        InstantiateWall(Width - 1, Height - 1, Rotation.ClockWise, MapTexture.WallCorner);
        InstantiateWall(Width - 1, 0, Rotation.Symmetry, MapTexture.WallCorner);

    }

    //Instantiate Get Quaternion rotation with Rotation's enum
    private Quaternion RotationVector(Rotation r)
    {
        switch (r)
        {
            case Rotation.ClockWise:
                return Quaternion.AngleAxis(-90,Vector3.forward);
            case Rotation.Symmetry:
                return Quaternion.AngleAxis(180,Vector3.forward);
            case Rotation.AntiClockWise:
                return Quaternion.AngleAxis(90,Vector3.forward);
            default:
                return Quaternion.AngleAxis(0,Vector3.forward);
        }
    }
    
    //Instantiate Wall and add it in the grid
    private void InstantiateWall(int x, int y, Rotation rotation, Sprite sprite)
    {
        Transform wall = Instantiate(WallObj, new Vector3(x, y, 0f), RotationVector(rotation), transform);
        SpriteRenderer wallSprite = wall.GetComponent<SpriteRenderer>();
        wallSprite.sprite = sprite;
        
        RoomData.ModifyGrid(x, y, wall);
    }
}
