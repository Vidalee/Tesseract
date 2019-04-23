using System.Collections;
using UnityEngine;

public class FeetCollision : MonoBehaviour
{    
    #region Variable

    private SpriteRenderer spriteRenderer;

    #endregion

    #region Initialise

    private void Awake()
    {
        spriteRenderer = transform.parent.GetComponentInChildren<SpriteRenderer>();
    }

    #endregion

    #region Collision

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Perspective"))
        {
            SpriteRenderer otherSpriteRenderer = other.transform.parent.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = otherSpriteRenderer.sortingOrder - 1;
        }
    }

    private IEnumerator OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Perspective"))
        {
            yield return new WaitForSeconds(0.5f);
            if((other.transform.position - transform.position).magnitude > 0.7f) spriteRenderer.sortingOrder = 100;
        }
    }

    #endregion
}
