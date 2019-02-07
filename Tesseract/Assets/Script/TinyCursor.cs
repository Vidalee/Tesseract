using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(cursorPos.y, cursorPos.x);
        float tinyY = Mathf.Sin(angle) * 1f;
        float tinyX = Mathf.Cos(angle) * 1f;
        cursorPos -= new Vector2(tinyX, tinyY);
        Vector2 center = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        float radius = 1f;
        //PLSFIX
        if (Mathf.Abs(Input.mousePosition.x - center.x) < radius && Mathf.Abs(Input.mousePosition.y - center.y) < radius)
            cursorPos = center;
        transform.position = cursorPos;
    }
}
