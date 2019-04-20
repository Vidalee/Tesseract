using UnityEngine;
using Random = UnityEngine.Random;

public class RoomInstance : MonoBehaviour
{
    public Transform Wall;
    public Transform SimpleDeco;
    public SimpleDecoration[] SimpleDecoration;

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

    public void AddSimpleDecoration(int number)
    {
        int len = SimpleDecoration.Length;
        MapGridCreation script = transform.parent.GetComponent<MapGridCreation>();

        for (int i = 0; i < number; i++)
        {
            int x = _roomData.X1 + Random.Range(1, _roomData.Width - 2);
            int y = _roomData.Y1 + Random.Range(1, _roomData.Height - 2);

            if (!script.Instances[y, x])
            {
                if (!script.Instances[y + 1, x] ||
                    !script.Instances[y + 1, x] ||
                    !script.Instances[y, x + 1] ||
                    !script.Instances[y, x + 1])
                {
                    _roomData.ModifyGrid(y - _roomData.Y1, x - _roomData.X1 , InstantiateDeco(x, y, SimpleDecoration[Random.Range(0, len)]));
                }
            }
            else
            {
                i--;
            }
        }
    }
    
    private Transform InstantiateDeco(int x, int y, SimpleDecoration deco)
    {
        MapGridCreation script = transform.parent.GetComponent<MapGridCreation>();
        Transform o = Instantiate(SimpleDeco, new Vector3(x, y, 0), Quaternion.identity, transform);

        o.GetComponent<SpriteRenderer>().sprite = deco.Sprite;
        if (deco.AsCol)
        {
            o.gameObject.AddComponent<EdgeCollider2D>().points = deco.Col;
            script.AddToInstance(y, x, true, false);
        }
        else
        {
            script.AddToInstance(y, x, true, true);
        }

        return o;
    }
}
