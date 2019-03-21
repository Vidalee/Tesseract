using System.Collections.Generic;
using UnityEngine;

public class InstanciateRoom : MonoBehaviour
{
    public int GameH;
    public int GameW;
    [SerializeField] protected LayerMask BlockingLayer;
    [SerializeField] protected Transform Floor;

    private List<Transform> _roomList;
    
    public int number;
    public int maxH;
    public int minH;
    public int maxW;
    public int minW;

    public Transform room;

    private void Start()
    {
        _roomList = new List<Transform>();
        GenerateRandomRoom();
    }

    public void GenerateRandomRoom()
    {
        for (int i = 0; i < number; i++)
        {
            int roomH = Random.Range(minH, maxH);
            int roomW = Random.Range(minW, maxW);

            //int xPos = Random.Range(0, GameH - roomH - 2);
            //int yPos = Random.Range(0, GameW - roomW - 2);

            int xPos = -2;
            int yPos = -2;
            
            if (PlaceEmpty(xPos, yPos, roomH, roomW))
            {
                Debug.Log("Touch");
                continue;
            }
            Transform o = Instantiate(room, new Vector3(xPos, yPos), Quaternion.identity.normalized, transform);
            _roomList.Add(o);
            
            
            RoomData rd = ScriptableObject.CreateInstance<RoomData>();
            //rd.InitiateGrid(roomH,roomW);
            o.GetComponent<GenerateRoomFloor>().Create(rd);
        }
    }

    public bool PlaceEmpty(int xPos, int yPos, int roomH, int roomW)
    {
        Vector3 pos1 = new Vector3(xPos, yPos);
        Vector3 pos2 = new Vector3(xPos, yPos + roomH);
        Vector3 pos3 = new Vector3(xPos + roomW, yPos + roomH);
        Vector3 pos4 = new Vector3(xPos + roomW, yPos);
        
        return Physics2D.Linecast(pos1, pos2, BlockingLayer) ||
               Physics2D.Linecast(pos2, pos3, BlockingLayer) ||
               Physics2D.Linecast(pos3, pos4, BlockingLayer) ||
               Physics2D.Linecast(pos4, pos1, BlockingLayer) ||
               Physics2D.Linecast(pos2, pos4, BlockingLayer) || 
               Physics2D.Linecast(pos1, pos3, BlockingLayer);
    }
}
