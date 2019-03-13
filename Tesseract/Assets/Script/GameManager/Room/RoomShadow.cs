using System.Collections.Generic;
using UnityEngine;

public class RoomShadow : MonoBehaviour
{
    public Transform shadowTop;
    public Transform shadowLeft;
    public Transform shafowRight;
    
    private int height;
    private int width;
    private Transform[,] room;
    private Transform[,] roomShadow;
    private float scale;
    
    private void Awake()
    {
        RoomCreation scriptParent = transform.parent.GetComponent<RoomCreation>();

        height = scriptParent.GetHeight();
        width = scriptParent.GetWidth();
        scale = scriptParent.GetScale();
        roomShadow = new Transform[height,width];
        
        CreateShadow();
    }

    private void CreateShadow()
    {
        for (int i = 1; i < height-1; i++)
        {
            InstantiateObject(1, i, shadowLeft);
            InstantiateObject(width - 2, i, shafowRight);
        }

        for (int i = 1; i < width - 1; i++)
        {
            InstantiateObject(i, height - 2, shadowTop);
        }
    }

    public void DestroyShadow(int x, int y)
    {
        if(roomShadow[y, x] != null)
            Destroy(roomShadow[y, x].gameObject);
    }

    private Transform InstantiateObject(int x, int y, Transform o)
    {
        //Instantiate object, set pos and add it to the room array
        Vector3 pos = transform.position + new Vector3(x, y, 0f) * scale;
        Transform obj = Instantiate(o, pos, Quaternion.identity,transform);
        roomShadow[y, x] = obj;
        return obj;
    }
}
