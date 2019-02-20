using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TreeEditor;
using UnityEngine;
using Random = UnityEngine.Random;


public class RoomCreation : MonoBehaviour
{
    public int maxHeight;
    public int maxWidth;
    public int minHeight;
    public int minWidth;
    public Transform floorObj;
    public Transform wallObj;
    public Sprite wallLeft;
    public Sprite wallRight;
    public Sprite wallTop;
    public Sprite wallBottom;
    public Sprite wallSprite;
    float scale;


    private Transform[,] room;
    private int height;
    private int width;
    private Transform door;

    // Start is called before the first frame update
    void Start()
    {
        InitiateRoom();
        SetSprite();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void InitiateRoom()
    {
        height = Random.Range(minHeight, maxHeight);
        width = Random.Range(minWidth, maxWidth);
        scale = floorObj.GetComponent<SpriteRenderer>().bounds.size.x;
        room = new Transform[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Vector3 pos = transform.position + new Vector3(j, i, 0f) * scale;
                Transform o = floorObj;
                if (i == 0 || i == height - 1 || j == 0 || j == width - 1) o = wallObj;
                Transform obj = Instantiate(o, pos, Quaternion.identity);
                obj.parent = transform;
                room[i, j] = obj;
            }
        }
    }
    void SetSprite()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (i == 0) room[i, j].GetComponent<SpriteRenderer>().sprite = wallBottom;
                if (i == height-1) room[i, j].GetComponent<SpriteRenderer>().sprite = wallTop;
                if (j == 0) room[i, j].GetComponent<SpriteRenderer>().sprite = wallLeft;
                if (j == width-1) room[i, j].GetComponent<SpriteRenderer>().sprite = wallRight;
            }
        }
        room[0,0].GetComponent<SpriteRenderer>().sprite = wallSprite;
        room[height-1,0].GetComponent<SpriteRenderer>().sprite = wallSprite;
        room[0,width-1].GetComponent<SpriteRenderer>().sprite = wallSprite;
        room[height-1,width-1].GetComponent<SpriteRenderer>().sprite = wallSprite;
    }
}
