using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Map/Data")]
public class MapData : ScriptableObject
{
    #region Variable

    private RoomData[] _roomsData;


    #endregion

    #region Set/Get

    public RoomData[] RoomsData
    {
        get => _roomsData;
        set => _roomsData = value;
    }

    #endregion
}
