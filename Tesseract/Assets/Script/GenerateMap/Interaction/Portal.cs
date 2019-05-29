using System.Collections;
using System.Collections.Generic;
using Script.GlobalsScript;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private PortalData _portalData;
    private Vector3 _pos;
    private Animator _a;

    public void Create(PortalData portalData, Vector3 pos)
    {
        _portalData = portalData;
        _pos = pos;
        GetComponent<SpriteRenderer>().sortingOrder = (int) (transform.position.y * -100);
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
            if (_portalData.IsBoss && StaticData.ActualFloor == StaticData.NumberFloor)
            {
                StaticData.actualData = other.GetComponentInParent<PlayerManager>().PlayerData;
                StaticData.ActualFloor++;
                ChangeScene.ChangeToScene("Boss");
                return;
            }
            if (_portalData.IsBoss)
            {
                StaticData.actualData = other.GetComponentInParent<PlayerManager>().PlayerData;
                Random.InitState(StaticData.Seed);

                StaticData.ActualFloor++;
                StaticData.Seed = Random.Range(0, 1000000);
                ChangeScene.ChangeToScene("Dungeon");
                return;
            }
            
            other.transform.parent.position = _pos;
        }
    }
}
