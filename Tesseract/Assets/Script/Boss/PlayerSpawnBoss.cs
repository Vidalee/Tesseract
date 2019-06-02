using System.Collections;
using Script.GlobalsScript;
using UnityEngine;

public class PlayerSpawnBoss : MonoBehaviour
{
    public Transform PlayerManager;
    public GameObject boss;

    private void Start()
    {
        Coffre.Créer();
        Transform o = Instantiate(PlayerManager, new Vector3(5, 5, 0), Quaternion.identity);
        o.GetComponent<PlayerManager>().Create(10, 2, int.Parse((string) Coffre.Regarder("id")), (string) Coffre.Regarder("mode") == "solo");
    }
}
