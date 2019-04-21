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

    private void OnEnable()
    {
        isOpen = false;
        if (boxCol.Length == 0)
        {
            boxCol = new[]
            {
                new Vector2(-0.5f, -0.5f), new Vector2(-0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, -0.5f), new Vector2(-0.5f, -0.5f)
            };
        }

        if (triggerCol.Length == 0)
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

    public Vector2[] PersCol => persCol;

    public GamesItem Item => item;

    public bool IsOpen
    {
        get => isOpen;
        set => isOpen = value;
    }
}
