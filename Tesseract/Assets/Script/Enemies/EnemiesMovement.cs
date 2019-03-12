using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    public int Speed;
    public int distance;
    
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
 
    private void FixedUpdate()
    {
        Displacement();
    }

    private void Displacement()
    {
        if ((player.transform.position - transform.position).sqrMagnitude < distance*distance)
        {
            return;
        }
        
        Vector2 displacement = (player.transform.position - transform.position).normalized * Time.deltaTime * Speed;
        
        transform.Translate(displacement);
    }
}