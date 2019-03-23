using UnityEngine;

public class Projectiles : MonoBehaviour
{
    private Animator _a;
    private ProjectilesData _projectilesData;
    
    public ProjectilesData ProjectilesData => _projectilesData;

    public void Create(ProjectilesData data)
    {
        _projectilesData = data;
    }

    private void FixedUpdate()
    {
        transform.Translate(_projectilesData.Direction * Time.deltaTime * _projectilesData.Speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag(_projectilesData.Tag))
        {
            Destroy(gameObject);
            other.transform.GetComponent<EnemiesLive>().GetDamaged(_projectilesData.Damage);
        }
    }
}