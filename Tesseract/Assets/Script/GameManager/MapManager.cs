using System.Collections.Generic;
using Script.Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public Transform roomObj;
    public Transform roadObj;
    public int maxHeight;
    public int maxWidth;
    public int minHeight;
    public int minWidth;
    public int minDistance;
    public int maxDistance;
    public int minrndDistance;
    public int maxrndDistance;
    public int roomNumber;
    public float scale;
    public LayerMask blockingLayer;
    
    private List<Transform> roomList;    
    
    // Start is called before the first frame update
    public void Awake()
    {
        roomList = new List<Transform>();
        InitiateEmptyRoom(Vector3.zero, 15, 15);
        
        for (int i = 0; i < roomNumber - 1; i++)
        {
            if (!ExpandMap()) i--;
        }
        //TODO AllNodes.CreateLinksBetweenNodes();
    }

    private Transform InitiateEmptyRoom(Vector3 pos, int height, int width)
    {
        //Instantiate an empty room
        Transform obj = Instantiate(roomObj,pos,Quaternion.identity,transform);
        roomList.Add(obj);  
        obj.GetComponent<RoomCreation>().CreateRoom(height,width);
        return obj;
    }

    private Transform InitiateDoor(Transform o, int cardinal)
    {
        RoomCreation script = o.GetComponent<RoomCreation>();
        
        return script.CreateDoor(cardinal);;        
    }

    private void LinkedRoom(bool direction, Transform door1, Transform door2)
    {
        //set door to linked
        door1.GetComponent<Door>().SetLinked(true);
        door2.GetComponent<Door>().SetLinked(true);
        
        //Instantiate Road an call the script
        Transform o = Instantiate(roadObj, Vector3.zero, Quaternion.identity, transform);
        o.GetComponent<Corridors>().Create(direction,door1,door2);
    }

    private bool ExpandMap()
    {
        //Choose random room
        int r = Random.Range(0, roomList.Count);
        Transform room1 = roomList[r];
        
        //Get height and width of room1 and set height and width of room2
        RoomCreation script = room1.GetComponent<RoomCreation>();
        
        int height1 = script.GetHeight();
        int width1 = script.GetWidth();
        int height2 = Random.Range(minHeight, maxHeight);
        int width2 = Random.Range(minWidth, maxWidth);
        
        //Choose distance between door (x or y) and distance between room (y or x)
        float distance = Random.Range(minDistance,maxDistance);
        float rndDistance = Random.Range(minrndDistance,maxrndDistance);
        
        //Choose pos for door1 and door2 (N/S/W/E)
        int cardinal1 = Random.Range(0, 4);
        int cardinal2 = (cardinal1 + 2) % 4;

        //Check if there is already a door
        foreach (var c in script.GetCardinal()) if (c == cardinal1) return false;
        Vector3 pos;
        script.AddCardinal(cardinal1);
        
        //Choose pos to instantiate the room
        switch (cardinal1)
        {
            case 0:
                pos = room1.position + new Vector3(rndDistance,height1 + distance) * scale;
                break;
            case 1:
                pos = room1.position + new Vector3(width1 + distance,rndDistance) * scale;
                break;
            case 2:
                pos = room1.position + new Vector3(rndDistance,-(height2 + distance)) * scale;
                break;
            default:
                pos = room1.position + new Vector3(-(width2 + distance),rndDistance) * scale;
                break;
        }

        //Check if the place is empty
        if (PlaceEmpty(pos, height2, width2)) return false;
        
        //Instantiate room1
        Transform room2 = InitiateEmptyRoom(pos,height2,width2);
        
        //Instantiate door1 and door2
        Transform door1 = InitiateDoor(room1, cardinal1);
        Transform door2 = InitiateDoor(room2, cardinal2);

        //Linked door1 and door2
        LinkedRoom(cardinal1 % 2 == 0, door1, door2);
        
        return true;
    }

    private bool PlaceEmpty(Vector3 pos, int height, int width)
    {
        //Create pos of all edge of room2
        Vector3 pos1 = pos;
        Vector3 pos2 = pos + new Vector3(0, height+1, 0) * scale;
        Vector3 pos3 = pos + new Vector3(width+1, height+1, 0) * scale;
        Vector3 pos4 = pos + new Vector3(width+1, 0, 0) * scale;

        //Create raycast
        RaycastHit2D linecast1 = Physics2D.Linecast(pos1, pos2, blockingLayer);
        RaycastHit2D linecast2 = Physics2D.Linecast(pos2, pos3, blockingLayer);
        RaycastHit2D linecast3 = Physics2D.Linecast(pos3, pos4, blockingLayer);
        RaycastHit2D linecast4 = Physics2D.Linecast(pos4, pos1, blockingLayer);
        RaycastHit2D linecast5 = Physics2D.Linecast(pos1, pos3, blockingLayer);
        
        //Return if raycast touch something
        return linecast1 || linecast2 || linecast3 || linecast4 || linecast5;
    }
}
