using UnityEngine;

public class Projectiles : MonoBehaviour
{
    private Animator _a;
    private ProjectilesData _projectilesData;
    public Transform ProjectilesAnimation;

    public void Create(ProjectilesData data)
    {
        _projectilesData = data;
        Transform o = Instantiate(ProjectilesAnimation, transform.position, Quaternion.identity, transform);
        o.GetComponent<ProjectilesAnimation>().Create(_projectilesData);
    }

    private void FixedUpdate()
    {
        Translate();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Obstacle"))
        {
            if(_projectilesData.Live-- <= 0) Destroy(gameObject);
        }

        if (_projectilesData.Tag != "" && other.gameObject.CompareTag(_projectilesData.Tag))
        {
            if(_projectilesData.Live-- <= 0) Destroy(gameObject);
            other.transform.GetComponent<EnemiesLive>().GetDamaged(_projectilesData.Damage);
        }
    }

    public void Translate()
    {
        transform.Translate(_projectilesData.Direction * Time.deltaTime * _projectilesData.Speed);
    }
}