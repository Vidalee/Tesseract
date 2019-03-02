using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridors : MonoBehaviour
{
    public float epsilon;
    public float scale;
    public Transform door1;
    public Transform door2;
    public bool direction;
    public Transform floorTransform;
    public Transform wallTransform;
    
    private List<Vector3> RoadFloor;
    private List<Vector3> RoadWall;

    private void Start()
    {
        Create(direction,door1,door2);
    }

    public void Create(bool direction, Transform door1,Transform door2)
    {
        this.direction = direction;
        this.door1 = door1;
        this.door2 = door2;
        
        InitiatePositionForLine();
        CreateRoad(RoadFloor,floorTransform);
        CreateRoad(RoadWall,wallTransform);
    }
    
    void InitiatePositionForLine()
    {
        //Value
        float xmid = (door1.position.x + door2.position.x)/2;
        float ymid = (door1.position.y + door2.position.y)/2;

        float xTest = Math.Abs(xmid) % scale;
        float yTest = Math.Abs(ymid) % scale;

        if (xTest > scale/4 && xTest < scale/4 * 3) xmid += scale / 2;
        if (yTest > scale/4 && yTest < scale/4 * 3) ymid += scale / 2;
        
        int xDiff = door1.position.x < door2.position.x ? 1 : -1;
        int yDiff = door1.position.y > door2.position.y ? 1 : -1;
        float addScale = scale * xDiff * yDiff;
        
        //Point for the floor
        Vector3 pointFloor1;
        Vector3 pointFloor2;
        Vector3 addWall = !direction ? new Vector3(0, scale, 0) : new Vector3(scale, 0, 0);

        //point for the top wall
        Vector3 pointWall1 = door1.position + addWall;
        Vector3 pointWall2;
        Vector3 pointWall3;
        Vector3 pointWall4 = door2.position + addWall;

        //point for the bottom wall
        Vector3 pointWall5 = door1.position - addWall;
        Vector3 pointWall6;
        Vector3 pointWall7;
        Vector3 pointWall8 = door2.position - addWall;
        
        //Create the point for wall and floor depending on the road we want
        if (!direction)
        {
            pointFloor1 = new Vector3(xmid, door1.position.y, 0);
            pointFloor2 = new Vector3(xmid, door2.position.y, 0);
                
            pointWall2 = new Vector3(pointFloor1.x + addScale, pointWall1.y, 0);
            pointWall3 = new Vector3(pointWall2.x,pointWall4.y,0);
        
            pointWall6 = new Vector3(pointFloor1.x - addScale, pointWall5.y, 0);
            pointWall7 = new Vector3(pointWall6.x,pointWall8.y,0);
        }
        else
        {
            pointFloor1 = new Vector3(door1.position.x,ymid,0);
            pointFloor2 = new Vector3(door2.position.x,ymid,0);
                    
            pointWall2 = new Vector3(pointWall1.x, pointFloor1.y + addScale, 0);
            pointWall3 = new Vector3(pointWall4.x,pointWall2.y,0);
        
            pointWall6 = new Vector3(pointWall5.x, pointFloor1.y - addScale, 0);
            pointWall7 = new Vector3(pointWall8.x,pointWall6.y,0);
        }
        
        //Road pos floor list
        RoadFloor = new List<Vector3>
        {
            door1.position, pointFloor1, pointFloor2, door2.position
        };
        
        //Road pos wall list
        RoadWall =  new List<Vector3>
        {
            pointWall1,pointWall2,pointWall3,pointWall4,
            pointWall5,pointWall6,pointWall7,pointWall8
        };
    }

    //Create a line between 2 points
    void Line(Vector3 point1, Vector3 point2, Transform t)
    {
        if (Math.Abs(point1.x - point2.x) < 0.01  && Math.Abs(point1.y - point2.y) < 0.01) return;
        
        int stop = 100;
        bool dir = Math.Abs(point1.y - point2.y) < Mathf.Epsilon;
        if (dir && point1.x > point2.x || !dir && point1.y > point2.y)
        {
            var save = point1;
            point1 = point2;
            point2 = save;
        }
        Vector3 add = dir? new Vector3(scale,0,0) : new Vector3(0,scale,0);
        
        point1 += add;
        while ((point1 - point2).sqrMagnitude > epsilon && stop > 0)
        {
            stop--;
            Transform f = Instantiate(t, point1, Quaternion.identity,transform);
            point1 += add;
        }
    }

    //Create Road
    void CreateRoad(List<Vector3> road, Transform o)
    {
        for (int i = 0; i < road.Count; i+=4)
        {
            Line(road[i],road[i+1],o);
            Instantiate(o, road[i+1], Quaternion.identity,transform);
            Line(road[i+1],road[i+2],o);
            Instantiate(o, road[i+2], Quaternion.identity,transform);
            Line(road[i+2],road[i+3],o);
        }
    }
}
