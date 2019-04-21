using UnityEngine;

public class LayerSorter : MonoBehaviour
{
    #region Variable

    private SpriteRenderer spriteRenderer;

    #endregion

    #region Initialise

    private void Awake()
    {
        spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    #endregion

    #region Collision

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacles"))
        {
            Debug.Log("Obstacles");
            SpriteRenderer otherSpriteRenderer = other.transform.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = otherSpriteRenderer.sortingLayerID - 1;
        }
    }

    #endregion
}
