using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective : MonoBehaviour
{

    public void Create(Vector2[] col)
    {
        GetComponent<EdgeCollider2D>().points = col;
    }
}
