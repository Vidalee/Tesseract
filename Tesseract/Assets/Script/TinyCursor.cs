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
        Vector2 playerPosition = player.transform.position;
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 tinyCursorPosition = playerPosition - cursorPosition;

        if (tinyCursorPosition.magnitude < distanceCursorTinyCursor) transform.position = playerPosition;
        else transform.position = cursorPosition + tinyCursorPosition.normalized * distanceCursorTinyCursor;
    }
}
