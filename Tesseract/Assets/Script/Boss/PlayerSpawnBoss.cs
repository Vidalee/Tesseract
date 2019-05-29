using Script.GlobalsScript;
using UnityEngine;

public class PlayerSpawnBoss : MonoBehaviour
{
    public Transform PlayerManager;
    public PlayerData data;

    private void Start()
    {
        StaticData.actualData = data;
        Instantiate(PlayerManager, new Vector3(5, 5, 0), Quaternion.identity).GetComponent<PlayerManager>().Create(10, 2);
    }
}
