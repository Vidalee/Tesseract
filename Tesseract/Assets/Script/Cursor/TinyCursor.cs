using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyCursor : MonoBehaviour
{
    private GameObject player;
    
    public float distanceCursorTinyCursor;
    
    void Start()
    {
        Cursor.visible = false;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 tinyCursorPos = playerPos - cursorPos;

        if (tinyCursorPos.magnitude < distanceCursorTinyCursor) transform.position = playerPos;
        else transform.position = cursorPos + tinyCursorPos.normalized * distanceCursorTinyCursor;
    }
}
