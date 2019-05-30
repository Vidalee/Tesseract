using UnityEngine;

public class Chest : MonoBehaviour
{
    public ChestData _chestData;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        Create(_chestData);
        GetComponent<SpriteRenderer>().sortingOrder = (int) (transform.position.y * -10);
    }

    public void Create(ChestData chestData)
    {
        _chestData = chestData;
       _spriteRenderer = GetComponent<SpriteRenderer>();
        
        Initialise();
    }

    public void OpenChest()
    {
        _spriteRenderer.sprite = _chestData.ChestOpen;
    }

    private void Initialise()
    {
        _spriteRenderer.sprite = _chestData.ChestClose;
        GetComponentInChildren<ChestInteraction>().Create(_chestData);
        
        GetComponent<EdgeCollider2D>().points = _chestData.BoxCol;
    }
}
