using System.Collections.Generic;
using UnityEngine;

public class RoomDecoration : MonoBehaviour
{
    public List<Transform> decorationList;
    
    private int height;
    private int width;
    private Transform[,] room;
    private Transform[,] roomDecoration;
    private float scale;

    private void Start()
    {
        RoomCreation scriptParent = transform.parent.GetComponent<RoomCreation>();

        height = scriptParent.GetHeight();
        width = scriptParent.GetWidth();
        scale = scriptParent.GetScale();
        roomDecoration = new Transform[height,width];
        
        CreateDecoration(5);
    }

    private void CreateDecoration(int number)
    {
        int decorationNumber = decorationList.Count;
        for (int i = 0; i < number; i++)
        {
            int xRand = Random.Range(2, width - 2);
            int yRand = Random.Range(2, height - 2);

            int decorationRand = Random.Range(0, decorationNumber);

            if (roomDecoration[yRand, xRand] == null)
            {
                InstantiateObject(xRand, yRand, decorationList[decorationRand]);
            }
            else
            {
                i--;
            }
        }
    }
    
    private Transform InstantiateObject(int x, int y, Transform o)
    {
        //Instantiate object, set pos and add it to the room array
        Vector3 pos = transform.position + new Vector3(x, y, 0f) * scale;
        Transform obj = Instantiate(o, pos, Quaternion.identity,transform);
        roomDecoration[y, x] = obj;
        return obj;
    }
}
