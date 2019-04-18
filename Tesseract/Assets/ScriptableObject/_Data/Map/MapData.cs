using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Map/Data")]
public class MapData : ScriptableObject
{
    private RoomData[] _roomsData;

    public RoomData[] RoomsData
    {
        get => _roomsData;
        set => _roomsData = value;
    }
}
