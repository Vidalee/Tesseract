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
        GameObject g = GameObject.Find("Player");
        PlayerMovement pm = g.GetComponent<PlayerMovement>();
        Vector3 center = pm.transform.position;
        
        Vector2 cursorPosR = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(cursorPosR.y - center.y, cursorPosR.x - center.x);
        float tinyY = Mathf.Sin(angle) * 1f;
        float tinyX = Mathf.Cos(angle) * 1f;
        Vector2 cursorPos = cursorPosR - new Vector2(tinyX, tinyY);
        //Vector2 center = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        
        if (pm == null) Debug.Log(":(");
        else {
            if (Vector2.Distance(cursorPosR, center) < 1f)
                cursorPos = center;
            transform.position = cursorPos;
        }
        
    }
}
