using UnityEngine;

public class GenerateRoomFloor : MonoBehaviour
{
    public Transform WallObj;
    public Transform FloorObj;
    
    [SerializeField] protected MapTextureData MapTexture;
    private RoomData RoomData;
    private Transform[,] grid;

    enum Rotation
    {
        Null,
        ClockWise,
        AntiClockWise,
        Symmetry
    };
    
    public void Create(RoomData roomData)
    {
        RoomData = roomData;
        CreateFloor();
    }

    private void CreateFloor()
    {
        //Instantiate floor
        for (int i = 0; i < RoomData.Height; i++)
        {
            for (int j = 0; j < RoomData.Width; j++)
            {
                Transform floor = Instantiate(FloorObj,new Vector3(i, j, 0f), Quaternion.identity, transform);
                floor.GetComponent<SpriteRenderer>().sprite = MapTexture.Floor[0];
            }
        }
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
