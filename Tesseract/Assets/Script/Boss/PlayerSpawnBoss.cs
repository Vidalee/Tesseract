using Script.GlobalsScript;
using UnityEngine;

public class PlayerSpawnBoss : MonoBehaviour
{
    public Transform PlayerManager;

    private void Start()
    {
        Instantiate(PlayerManager, new Vector3(5, 5, 0), Quaternion.identity).GetComponent<PlayerManager>().Create(10, 2, int.Parse((string) Coffre.Regarder("id")), (string) Coffre.Regarder("mode") == "solo");
    }
}
