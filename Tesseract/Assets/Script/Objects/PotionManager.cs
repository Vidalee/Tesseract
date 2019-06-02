using System.Collections;
using Script.GlobalsScript.Struct;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    private Potions _potion;
    private SpriteRenderer _spriteRenderer;
    public GameEvent AddItem;
    public GameEvent AthItem;
    public GameEvent AthItemS;
    public bool wait;

    public void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Create(Potions potion)
    {
        _potion = potion;
        _spriteRenderer.sprite = potion.icon;
        transform.localScale *= 2;
        _spriteRenderer.sortingOrder = (int) transform.position.y * -15;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mouse"))
        {
            AthItem.Raise(new EventArgsItemAth(_potion));
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Mouse"))
        {
            AthItemS.Raise(new EventArgsItemAth(_potion));
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if ((other.transform.position - transform.position).sqrMagnitude < 0.5)
            {
                if (!wait && Input.GetKey(KeyCode.F))
                {

                    StartCoroutine(Wait());
                    AddItem.Raise(new EventArgsItem(_potion, transform));
                }
            }
        }
    }

    IEnumerator Wait()
    {
        wait = true;
        yield return new WaitForSeconds(0.5f);
        wait = false;
    }
}
