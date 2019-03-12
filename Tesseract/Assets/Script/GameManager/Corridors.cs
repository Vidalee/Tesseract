using System;
using UnityEngine;

public class Corridors : MonoBehaviour
{
    public float epsilon;
    public float scale;
    public Transform door1;
    public Transform door2;
    public bool direction;

    public Transform floorObj;
    public Transform wallLeft;
    public Transform wallRight;
    public Transform wallTop;
    public Transform wallBottom;
    public Transform wallBottomLeft;
    public Transform wallBottomRight;
    public Transform wallTopLeft;
    public Transform wallTopRight;
    public Transform wallCornerBottomLeftT;
    public Transform wallCornerBottomRightT;
    public Transform wallCornerTopLeftB;
    public Transform wallCornerTopRightB;

    
    private Vector3[] RoadFloor;
    private Vector3[] RoadWallTop;
    private Vector3[] RoadWallBottom;
    private Transform[] RoadSpriteTop;
    private Transform[] RoadSpriteBottom;

    public void Create(bool direction, Transform door1,Transform door2)
    {
        this.direction = direction;
        this.door1 = door1;
        this.door2 = door2;
        
        InitiatePositionForLine();
        CreateFloor(RoadFloor);
        CreateRoad(RoadWallTop, RoadSpriteTop);
        CreateRoad(RoadWallBottom, RoadSpriteBottom); 
    }
    
    void InitiatePositionForLine()
    {
        //Value
        float xMid = (door1.position.x + door2.position.x)/2;
        float yMid = (door1.position.y + door2.position.y)/2;

        float xTest = Math.Abs(xMid) % scale;
        float yTest = Math.Abs(yMid) % scale;

        if (xTest > scale/4 && xTest < scale/4 * 3) xMid += scale / 2;
        if (yTest > scale/4 && yTest < scale/4 * 3) yMid += scale / 2;
        
        int xDiff = door1.position.x < door2.position.x ? 1 : -1;
        int yDiff = door1.position.y > door2.position.y ? 1 : -1;
        float addScale = scale * xDiff * yDiff;
        
        //Point for the floor
        Vector3 perspective = new Vector3(0, scale / 2, 0);
        Vector3 pointFloor0 = door1.position;
        Vector3 pointFloor1;
        Vector3 pointFloor2;
        Vector3 pointFloor3 = door2.position;
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
            pointFloor1 = new Vector3(xMid, door1.position.y, 0);
            pointFloor2 = new Vector3(xMid, door2.position.y, 0);

            pointWall2 = new Vector3(pointFloor1.x + addScale, pointWall1.y, 0) - perspective;
            pointWall3 = new Vector3(pointWall2.x,pointWall4.y,0) - perspective;
            
            pointWall1 -= perspective;
            pointWall4 -= perspective;
        
            pointWall6 = new Vector3(pointFloor1.x - addScale, pointWall5.y, 0);
            pointWall7 = new Vector3(pointWall6.x,pointWall8.y,0);

            Transform tCorner1 = wallTopRight;
            Transform tCorner2 = wallCornerBottomLeftT;
            Transform tCorner3 = wallCornerTopRightB;
            Transform tCorner4 = wallBottomLeft;
            
            Transform tTop = wallRight;
            Transform tBottom = wallLeft;

            if (door1.position.y < door2.position.y && door1.position.x < door2.position.x || 
                door1.position.y > door2.position.y && door1.position.x > door2.position.x)
            {
                tTop = wallLeft;
                tBottom = wallRight;
            }

            if (door1.position.y < door2.position.y && door1.position.x < door2.position.x)
            {
                tCorner1 = wallCornerBottomRightT;
                tCorner2 = wallTopLeft;
                tCorner3 = wallBottomRight;
                tCorner4 = wallCornerTopLeftB;
            }

            if (door1.position.y < door2.position.y && door1.position.x > door2.position.x)
            {
                tCorner1 = wallCornerBottomLeftT;
                tCorner2 = wallTopRight;
                tCorner3 = wallBottomLeft;
                tCorner4 = wallCornerTopRightB;
            }

            if (door1.position.y > door2.position.y && door1.position.x > door2.position.x)
            {
                tCorner1 = wallTopLeft;
                tCorner2 = wallCornerBottomRightT;
                tCorner3 = wallCornerTopLeftB;
                tCorner4 = wallBottomRight;
            }
            
            RoadSpriteTop = new[] {wallTop, tCorner1, tCorner2, tTop};
            RoadSpriteBottom = new[] {wallBottom, tCorner3, tCorner4, tBottom};
            
            if (Math.Abs(pointWall2.y - pointWall3.y) < 0.01)
            {
                RoadSpriteTop[1] = RoadSpriteTop[0];
                RoadSpriteTop[2] = RoadSpriteTop[0];

                RoadSpriteBottom[1] = RoadSpriteBottom[0];
                RoadSpriteBottom[2] = RoadSpriteBottom[0];
            }
        }
        else
        {
            pointFloor1 = new Vector3(door1.position.x,yMid,0);
            pointFloor2 = new Vector3(door2.position.x,yMid,0);
                    
            pointWall2 = new Vector3(pointWall1.x, pointFloor1.y + addScale, 0);
            pointWall3 = new Vector3(pointWall4.x,pointWall2.y,0);
        
            pointWall6 = new Vector3(pointWall5.x, pointFloor1.y - addScale, 0);
            pointWall7 = new Vector3(pointWall8.x,pointWall6.y,0);
            
            Transform tCorner1 = wallBottomRight;
            Transform tCorner2 = wallCornerTopLeftB;
            Transform tCorner3 = wallCornerBottomRightT;
            Transform tCorner4 = wallTopLeft;
            
            Transform tTop = wallBottom;
            Transform tBottom = wallTop;
            
            if (door1.position.y < door2.position.y && door1.position.x > door2.position.x || 
                door1.position.y > door2.position.y && door1.position.x < door2.position.x)
            {
                tTop = wallTop;
                tBottom = wallBottom;
            }

            if (door1.position.x > door2.position.x && door1.position.y > door2.position.y)
            {
                pointWall5 += perspective;
                pointWall6 -= perspective;
                pointWall7 -= perspective;
                pointWall8 -= perspective;
            }
            if (door1.position.x < door2.position.x && door1.position.y > door2.position.y)
            {
                pointWall1 += perspective;
                pointWall2 -= perspective;
                pointWall3 -= perspective;
                pointWall4 -= perspective;
                
                tCorner1 = wallCornerBottomLeftT;
                tCorner2 = wallTopRight;
                tCorner3 = wallBottomLeft;
                tCorner4 = wallCornerTopRightB;
            }
            
            if (door1.position.x < door2.position.x && door1.position.y < door2.position.y)
            {
                pointWall5 -= perspective;
                pointWall6 -= perspective;
                pointWall7 -= perspective;
                pointWall8 += perspective;
                
                tCorner1 = wallCornerTopLeftB;
                tCorner2 = wallBottomRight;
                tCorner3 = wallTopLeft;
                tCorner4 = wallCornerBottomRightT;
            }
            
            if (door1.position.x > door2.position.x && door1.position.y < door2.position.y)
            {
                pointWall1 -= perspective;
                pointWall2 -= perspective;
                pointWall3 -= perspective;
                pointWall4 += perspective;
                
                tCorner1 = wallTopRight;
                tCorner2 = wallCornerBottomLeftT;
                tCorner3 = wallCornerTopRightB;
                tCorner4 = wallBottomLeft;
            }

            RoadSpriteTop = new[] {wallRight, tCorner1, tCorner2, tTop};
            RoadSpriteBottom = new[] {wallLeft, tCorner3, tCorner4, tBottom};

            if (Math.Abs(pointWall2.x - pointWall3.x) < 0.01)
            {
                RoadSpriteTop[1] = RoadSpriteTop[0];
                RoadSpriteTop[2] = RoadSpriteTop[0];

                RoadSpriteBottom[1] = RoadSpriteBottom[0];
                RoadSpriteBottom[2] = RoadSpriteBottom[0];
            }
        }
        
        //Road pos floor list
        RoadFloor = new []
        {
            pointFloor0, pointFloor1, pointFloor2, pointFloor3
        };
        
        //Road pos wall list
        RoadWallTop =  new []
        {
            pointWall1,pointWall2,pointWall3,pointWall4,
        };
        
        RoadWallBottom =  new []
        {
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
            Instantiate(t, point1, Quaternion.identity,transform);
            point1 += add;
        }
    }

    //Create Road
    void CreateFloor(Vector3[] road)
    {
        Line(road[0],road[1],floorObj);
        Instantiate(floorObj, road[1], Quaternion.identity,transform);
        Line(road[1],road[2],floorObj);
        Instantiate(floorObj, road[2], Quaternion.identity,transform);
        Line(road[2],road[3],floorObj);
    }

    void CreateRoad(Vector3[] road, Transform[] t)
    {
        Line(road[0],road[1],t[0]);
        Instantiate(t[1], road[1], Quaternion.identity,transform);
        Line(road[1],road[2],t[3]);
        Instantiate(t[2], road[2], Quaternion.identity,transform);
        Line(road[2],road[3],t[0]);
    }
}