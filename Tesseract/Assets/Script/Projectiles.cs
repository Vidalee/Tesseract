using UnityEngine;

public class Projectiles : MonoBehaviour
{
    private Vector3 direction;
    private int speed;
    private int damage;

    public void Create(Vector3 direction, int speed, int damage)
    {
        this.direction = direction;
        this.speed = speed;
        this.damage = damage;
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Enemies"))
        {
            Destroy(gameObject);
            other.transform.GetComponent<EnemiesLive>().GetDamaged(damage);
        }
    }
}
