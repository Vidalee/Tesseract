using Script.GlobalsScript;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private PortalData _portalData;
    private Vector3 _pos;
    private Animator _a;

    public void Create(PortalData portalData, Vector3 pos)
    {
        _portalData = portalData;
        _pos = pos;
        GetComponent<SpriteRenderer>().sortingOrder = (int) (transform.position.y * -10);
        _a = GetComponent<Animator>();

        Initiate();
    }

    private void Initiate()
    {
        GetComponent<EdgeCollider2D>().points = _portalData.Col;
        
        AnimatorOverrideController aoc = new AnimatorOverrideController(_a.runtimeAnimatorController);
        AnimatorOverride.AnimationOverride("Default", _portalData.Animation, aoc, _a);
        _a.speed = 0.6f;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("PlayerFeet"))
        {
            StaticData.ActualFloor += 1;

            if (_portalData.IsBoss && StaticData.ActualFloor > StaticData.NumberFloor)
            {
                StaticData.actualData = other.GetComponentInParent<PlayerManager>().PlayerData;
                ChangeScene.ChangeToScene("Boss");
                return;
            }
            if (_portalData.IsBoss && StaticData.ActualFloor < 0)
            {
                StaticData.ActualFloor = 0;
                SaveSystem.SavePlayer(other.GetComponentInParent<PlayerManager>().PlayerData);
                SaveSystem.SaveGlobal();
                ChangeScene.ChangeToScene("LevelSelection");
                return;
            }
            if(_portalData.IsBoss)
            {
                StaticData.actualData = other.GetComponentInParent<PlayerManager>().PlayerData;
                Random.InitState(StaticData.Seed);

                StaticData.Seed = Random.Range(0, 1000000);
                ChangeScene.ChangeToScene("Dungeon");
                return;
            }
            
            other.transform.parent.position = _pos;
        }
    }
}
