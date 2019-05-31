using UnityEngine;

[CreateAssetMenu(fileName = "Chest", menuName = "Map/Chest")]
public class ChestData : ScriptableObject
{
    [SerializeField] protected Sprite chestClose;
    [SerializeField] protected Sprite chestOpen;
    [SerializeField] protected Vector2[] boxCol;
    [SerializeField] protected Vector2[] triggerCol;
    [SerializeField] protected Vector2[] persCol;
    [SerializeField] protected GamesItem item;
    private bool isOpen;

    public void Create(ChestData chestData)
    {
        chestClose = chestData.chestClose;
        chestOpen = chestData.chestOpen;
        boxCol = chestData.boxCol;
        triggerCol = chestData.triggerCol;
        persCol = chestData.persCol;
        isOpen = chestData.isOpen;
    }

    private void OnEnable()
    {
        isOpen = false;
        if (boxCol == null || boxCol.Length == 0)
        {
            boxCol = new[]
            {
                new Vector2(-0.5f, -0.5f), new Vector2(-0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, -0.5f), new Vector2(-0.5f, -0.5f)
            };
        }

        if (triggerCol == null || triggerCol.Length == 0)
        {
            triggerCol = new[]
            {
                new Vector2(-0.7f, -0.7f), new Vector2(-0.7f, 0.7f), new Vector2(0.7f, 0.7f), new Vector2(0.7f, -0.7f), new Vector2(-0.7f, -0.7f)
            };
        }
    }

    public Sprite ChestClose => chestClose;

    public Sprite ChestOpen => chestOpen;

    public Vector2[] BoxCol => boxCol;

    public Vector2[] TriggerCol => triggerCol;

    public GamesItem Item
    {
        get => item;
        set => item = value;
    }

    public bool IsOpen
    {
        get => isOpen;
        set => isOpen = value;
    }
}
