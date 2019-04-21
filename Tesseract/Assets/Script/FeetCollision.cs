using System.Collections;
using UnityEngine;

public class FeetCollision : MonoBehaviour
{
    private bool isIn;
    
    #region Variable

    private SpriteRenderer spriteRenderer;
    private int save;

    #endregion

    #region Initialise

    private void Awake()
    {
        spriteRenderer = transform.parent.GetComponentInChildren<SpriteRenderer>();
        save = spriteRenderer.sortingOrder;
    }

    #endregion

    #region Collision

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            SpriteRenderer otherSpriteRenderer = other.transform.parent.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = otherSpriteRenderer.sortingOrder - 1;
        }

        isIn = true;
    }

    private IEnumerator OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Obstacle"))
        {
            yield return new WaitForSeconds(1);
            if((other.transform.position - transform.position).magnitude > 0.8f) spriteRenderer.sortingOrder = 100;
        }
    }

    #endregion
}
