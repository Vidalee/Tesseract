using UnityEngine;

class RoomJoin : MonoBehaviour
{
    public void Join()
    {
        UDPRoomManager.Join(gameObject.name);
    }
}

