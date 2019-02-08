
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int Speed;
 
    private void FixedUpdate()
    {
        PlayerDisplacement();
    }

    private void PlayerDisplacement()
    {
        int xDir = (int) Input.GetAxisRaw("Horizontal");
        int yDir = (int) Input.GetAxisRaw("Vertical");
        Vector2 displacement = new Vector2(xDir,yDir) * Time.deltaTime * Speed;
        
        transform.Translate(displacement);
    }
}
