using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciateRoom : MonoBehaviour
{
    public int GameH;
    public int GameW;
    
    public int number;
    public int maxH;
    public int minH;
    public int maxW;
    public int minW;

    public Transform room;

    public void GenerateRandomRoom()
    {
        for (int i = 0; i < number; i++)
        {
            int roomH = Random.Range(minH, maxH);
            int roomW = Random.Range(minW, maxW);

            int xPos = Random.Range(0, GameH - roomH - 2);
            int yPos = Random.Range(0, GameW - roomW - 2);
            
            Transform o = Instantiate(room, new Vector2(xPos, yPos), Quaternion.identity.normalized, transform);

            RoomData rd = ScriptableObject.CreateInstance<RoomData>();
            rd.InitiateGrid(roomH,roomW);
            o.GetComponent<GenerateRoomFloor>().Create(rd);
        }
    }
}
