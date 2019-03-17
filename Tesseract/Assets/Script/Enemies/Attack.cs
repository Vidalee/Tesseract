using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected int distance;
    [SerializeField] protected float maxCooldown;

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