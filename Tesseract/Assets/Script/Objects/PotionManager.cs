using Script.GlobalsScript.Struct;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    private Potions _potion;
    private SpriteRenderer _spriteRenderer;
    public GameEvent AthItem;

    public void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log(_spriteRenderer);
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
            AthItem.Raise(new eventArgsItemAth(_potion));
        }
    }
}
