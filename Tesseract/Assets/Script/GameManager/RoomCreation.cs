﻿using System.Collections.Generic;
using UnityEngine;

public class RoomCreation : MonoBehaviour
{
    public Transform floorObj;
    public Transform wallLeft;
    public Transform wallRight;
    public Transform wallTop;
    public Transform wallBottom;
    public Transform wallTopLeft;
    public Transform wallTopRight;
    public Transform wallBottomLeft;
    public Transform wallBottomRight;
    public Transform doorObj;
    
    private int height;
    private int width;
    
    private List<Transform> doorCreated;
    private Transform[,] room;
    private List<int> cardinalDoor;
    private float scale;

    public int GetHeight() => height;
    public int GetWidth() => width;
    public List<Transform> GetDoorCreated() => doorCreated; 
    public List<int> GetCardinal() => cardinalDoor;

    public void CreateRoom(int height, int width)
    {
        //Set value, initiate List and instantiate a room
        this.height = height;
        this.width = width;
        scale = floorObj.GetComponent<SpriteRenderer>().bounds.size.x;
        room = new Transform[height, width];
        doorCreated = new List<Transform>();
        cardinalDoor = new List<int>();

        InitiateRoom();
    }
    public Transform CreateDoor(int cardinal)
    {      
        return InitiateDoor(cardinal);
    }
    public void AddCardinal(int cardinal)
    {
        cardinalDoor.Add(cardinal);
    }

    private Transform InstantiateObject(int x, int y, Transform o)
    {
        //Instantiate object, set pos and add it to the room array
        Vector3 pos = transform.position + new Vector3(x, y, 0f) * scale;
        Transform obj = Instantiate(o, pos, Quaternion.identity,transform);
        room[y, x] = obj;
        return obj;
    }

    private Transform InstantiateDoor(int cardinal, int x, int y)
    {
        //Replace door in room, instantiate it
        Destroy(room[y,x].gameObject);
        Transform door = InstantiateObject(x,y,doorObj);
        door.GetComponent<Door>().Create(cardinal);
        doorCreated.Add(door.GetComponent<Transform>());
        return door;
    }
    
    public void InitiateRoom()
    {       
        //Floor generation
        for (int y = 1; y < height - 1; y++) for (int x = 1; x < width - 1; x++) InstantiateObject(x,y,floorObj);
        
        //Left and right wall generation
        for (int y = 1; y < height - 1; y++) InstantiateObject(width - 1,y,wallRight);
        for (int y = 1; y < height - 1; y++) InstantiateObject(0,y,wallLeft);
        
        //Top and bottom wall generation
        for (int x = 1; x < width - 1; x++) InstantiateObject(x,height - 1,wallTop);
        for (int x = 1; x < width - 1; x++) InstantiateObject(x,0,wallBottom);
        
        //Corner wall generation
        InstantiateObject(0,0,wallBottomLeft);
        InstantiateObject(0,height - 1,wallBottomRight);
        InstantiateObject(width - 1, 0,wallTopLeft);
        InstantiateObject(width - 1, height - 1,wallTopRight);
    }
    private Transform InitiateDoor(int cardinal)
    {
        int xRandom = Random.Range(1, width - 1);
        int yRandom = Random.Range(1, height - 1);
        
        //Create a door depending of the cardinal
        switch (cardinal)
        {
            case 0:
                return InstantiateDoor(cardinal,xRandom, height - 1);
            case 1:
                return InstantiateDoor(cardinal,width - 1, yRandom);
            case 2:
                return InstantiateDoor(cardinal,xRandom, 0);
            default:
                return InstantiateDoor(cardinal,0, yRandom);
        }
    }
}
