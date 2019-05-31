using UnityEngine;

public class MousePotSelection : MonoBehaviour
{
    private int[] pos;
    private int actualIndex;

    private void Awake()
    {
        pos = new[] {-90, -30, 30, 90};
        actualIndex = 0;
    }

    private void Start()
    {
        transform.position = new Vector2(-90, -350);
    }

    private void FixedUpdate()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            UpdatePos(1);
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            UpdatePos(-1);
        }
    }

    private void UpdatePos(int i)
    {
        actualIndex = (actualIndex + i + 4) % 4;
        Debug.Log(pos[actualIndex]);
        Debug.Log(transform.position.x);
        transform.position = new Vector2(pos[actualIndex], transform.position.y);
    }
}
