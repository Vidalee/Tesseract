using UnityEngine;

public class PotionManager : MonoBehaviour
{
    private Potions _potion;
    private SpriteRenderer _spriteRenderer;

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
    }
}
