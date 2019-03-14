using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorter : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Obstacles"))
        {
            Debug.Log("Obstacles");
            SpriteRenderer otherSpriteRenderer = other.transform.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = otherSpriteRenderer.sortingLayerID - 1;
        }
    }
}
