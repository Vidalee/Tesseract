using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerDash : MonoBehaviour
{
    private GameObject player;
    private bool canDash;
    private float cooldown;

    public float maxCooldown;
    public float dashDistance;
    
    void Start()
    {
        player = GameObject.Find("Player");
        canDash = true;
        cooldown = maxCooldown;
    }

    void FixedUpdate()
    {
        if (Input.GetKey("space") && canDash)
        {
            canDash = false;
            cooldown = maxCooldown;
            Vector2 playerPos = player.transform.position;
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (cursorPos - playerPos).normalized * dashDistance;
            Vector2 endPos = playerPos + direction;
            
            player.transform.position = endPos;
        }

        if (!canDash)
        {
            if (cooldown-- <= 0) canDash = true;
        }
    }
}
