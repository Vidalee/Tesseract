using System.Reflection;
using UnityEngine;

public class RoomInstance : MonoBehaviour
{
    
    public Transform Wall;

    private RoomData _roomData;
    private int _prob;

    public void Create(RoomData roomData, int prob)
    {
        _roomData = roomData;
        _prob = prob;
    }

    public void BigWall()
    {
        int x = Random.Range(_roomData.X1 + 1, _roomData.X1 + _roomData.Width/2);
        int y = Random.Range(_roomData.Y1 + 1, _roomData.Y1 + _roomData.Height/2);

        int width = Random.Range(x + 2, _roomData.X2) - x;
        int height = Random.Range(y + 2, _roomData.Y2) - y;

        MapGridCreation script = transform.parent.GetComponent<MapGridCreation>();
        
        for (int i = x; i < x + width; i++)
        {
            for (int j = y; j < y + height; j++)
            {
                if (Random.Range(0, _prob + 1) == 0) continue;
                
                script.AddToGrid(j, i, false);
                _roomData.ModifyGrid(j - y, i - x, Wall);
            }
        }
    }
}
