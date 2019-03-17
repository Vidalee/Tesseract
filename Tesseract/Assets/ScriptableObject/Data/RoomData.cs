using UnityEngine;

[CreateAssetMenu(fileName = "RoomData", menuName = "Map/Room/Data")]
public class RoomData : ScriptableObject
{
    [SerializeField] protected int height;
    [SerializeField] protected int width;
    [SerializeField] protected Transform[,] gridObstacles;

    public int GetHeight() => height;
    public int GetWidth() => width;
    public Transform[,] GetGrid() => gridObstacles;

    public void InitiateGrid(int height, int width)
    {
        this.height = height;
        this.width = width;
        gridObstacles = new Transform[height,width];
    }

    public void ModifyGrid(int x, int y, Transform o)
    {
        gridObstacles[y, x] = o;
    }
}
