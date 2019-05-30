using UnityEngine;

public class MiniMapPos : MonoBehaviour
{
    public void Pos(Vector3 pos)
    {
        transform.position = pos;
        GetComponent<Camera>().orthographicSize = pos.x;
    }
}
