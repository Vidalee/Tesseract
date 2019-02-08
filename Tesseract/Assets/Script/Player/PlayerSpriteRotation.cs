using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRotation : MonoBehaviour
{
    void Update()
    {
        SpriteRotaton();
    }

    private void SpriteRotaton()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.up = direction;
    }
}
