using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private PortalData _portalData;
    private Vector3 _pos;

    public void Create(PortalData portalData, Vector3 pos)
    {
        _portalData = portalData;
        _pos = pos;
        GetComponent<SpriteRenderer>().sortingOrder = (int) (transform.position.y * -100);

        Initiate();
    }

    private void Initiate()
    {
        GetComponent<EdgeCollider2D>().points = _portalData.Col;
        GetComponent<SpriteRenderer>().sprite = _portalData.Sprite;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("PlayerFeet"))
        {
            other.transform.parent.position = _pos;
        }
    }
}
