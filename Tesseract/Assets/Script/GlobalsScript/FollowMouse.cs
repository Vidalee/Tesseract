using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
