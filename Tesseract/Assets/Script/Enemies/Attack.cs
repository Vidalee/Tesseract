using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    public int distance;
    public float maxCooldown;

    private float cooldown;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        TryAttack();
    }

    public void TryAttack()
    {
        if (cooldown < maxCooldown)
        {
            cooldown++;
            return;
        }
        
        if ((transform.position - player.transform.position).sqrMagnitude < distance*distance)
        {
            cooldown -= maxCooldown;
            Live script = player.GetComponent<Live>();
            script.Damage(damage);
        }
    }
}