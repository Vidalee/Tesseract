using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] protected AnimatorOverrideController Aoc;

    private Vector3 _direction;
    private float _speed;
    private int _damage;
    private string _tag;
    private AnimationClip _anim;

    public void Create(Vector3 direction, float speed, int damage, string tag, AnimationClip anim)
    {
        _direction = direction;
        _speed = speed;
        _damage = damage;
        _tag = tag;
        _anim = anim;
        
        AnimationOverride();
    }

    private void FixedUpdate()
    {
        transform.Translate(_direction * Time.deltaTime * _speed);
    }

    private void AnimationOverride()
    {
        Animator ac = GetComponent<Animator>();
        ac.runtimeAnimatorController = Aoc;
        Aoc["DefaultProjectiles"] = _anim;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag(_tag))
        {
            Destroy(gameObject);
            other.transform.GetComponent<EnemiesLive>().GetDamaged(_damage);
        }
    }
}