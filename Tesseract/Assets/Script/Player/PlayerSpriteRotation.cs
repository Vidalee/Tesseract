﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRotation : MonoBehaviour
{
    void Update()
    {
        SpriteRotation();
    }

    private void SpriteRotation()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.up = direction;
    }
}
