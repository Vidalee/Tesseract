using UnityEngine;

public class FeetCollision : MonoBehaviour
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
        if (other.transform.CompareTag("DoorTop"))
        {
            SpriteRenderer otherSpriteRenderer = other.transform.parent.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = otherSpriteRenderer.sortingOrder - 1;
        }
        
        if (other.transform.CompareTag("DoorBottom"))
        {
            Debug.Log("bot");
            SpriteRenderer otherSpriteRenderer = other.transform.parent.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = otherSpriteRenderer.sortingOrder + 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("DoorTop") || other.transform.CompareTag("DoorBottom"))
        {
            spriteRenderer.sortingOrder = 100;
        }
    }

    #endregion
}
