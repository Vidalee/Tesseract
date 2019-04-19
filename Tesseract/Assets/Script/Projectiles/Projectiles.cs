using UnityEngine;

public class Projectiles : MonoBehaviour
{
    #region Variable

    private Animator _a;
    private ProjectilesData _projectilesData;
    public Transform projectilesAnimation;

    #endregion

    #region Initialise

    public void Create(ProjectilesData data)
    {
        _projectilesData = data;
        Transform o = Instantiate(projectilesAnimation, transform.position, Quaternion.identity, transform);
        o.GetComponent<ProjectilesAnimation>().Create(_projectilesData);
    }

    #endregion

    #region Update

    private void FixedUpdate()
    {
        Translate();
    }

    #endregion

    #region Collision

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

        if (_projectilesData.Tag != "" && other.gameObject.CompareTag(_projectilesData.Tag))
        {
            if(_projectilesData.Live-- <= 0) Destroy(gameObject);
            other.transform.GetComponent<EnemiesLive>().GetDamaged(_projectilesData.Damage);
        }
    }

    #endregion

    #region Movement

    public void Translate()
    {
        transform.Translate(_projectilesData.Direction * Time.deltaTime * _projectilesData.Speed);
    }

    #endregion
}