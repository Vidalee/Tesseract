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
        Vector3 player = pm.transform.position;
        Vector2 cursorPos = new Vector2();
        Vector2 cursorPosR = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(cursorPosR, player) < 1f)
            cursorPos = player;
        else
        {
            float angle = Mathf.Atan2(cursorPosR.y - player.y, cursorPosR.x - player.x);
            float tinyY = Mathf.Sin(angle) * 1f;
            float tinyX = Mathf.Cos(angle) * 1f;
            cursorPos = cursorPosR - new Vector2(tinyX, tinyY);
        }

        transform.position = cursorPos;
    }
}
