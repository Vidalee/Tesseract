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
        //o.GetComponent<PlayerManager>().Create(10, 2, 0, true);
        boss.GetComponent<BossAttack>().Create(o.GetChild(0));
        boss.GetComponent<BossMovement>().Create(o.GetChild(0));
    }
}
