using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] protected Animator _a;
    [SerializeField] protected SpriteRenderer _sprite;

    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected float _notCloseEnough;
    [SerializeField] protected float _distDash;

    public Transform _player;

    public void Create(Transform player)
    {
        _player = player;
        Debug.Log(_player);
    }
    
    public void Update()
    {
        //_player = GameObject.Find("Player (Clone)").transform;
        //Debug.Log(_player);
        _sprite.sortingOrder = (int)(transform.position.y * -10);
        Vector3 distanceToPos = _player.position + new Vector3(0, -0.375f) - transform.position;
        float magnitude = distanceToPos.magnitude;
        if (magnitude > _notCloseEnough)
        {
            _a.SetBool("Dashing", true);
            _a.Play("PreDash");
            
            RaycastHit2D linecast = Physics2D.Linecast(transform.position, _player.position);
            StartCoroutine(Dash(linecast.point));
        }
        else if (magnitude > _attackRange)
        {
            _a.SetBool("Speed", true);
            StraightToPoint(distanceToPos.normalized);
        }
        else
        {
            _a.SetBool("Speed", false);
        }
    }

    public IEnumerator Dash(Vector3 direction)
    {  
        float step = _distDash * Time.fixedDeltaTime;
        float t = 0;
        Vector3 end = transform.position + direction - direction * 0.01f;

        while ((end - transform.position).magnitude > 0.1f)
        {
            t += step;
            transform.position = Vector3.Lerp(transform.position, end, t);
            yield return new WaitForFixedUpdate();
        }

        transform.position = end;
        _a.SetBool("Dashing", false);
    }
    
    private void StraightToPoint(Vector3 direction)
    {
        transform.Translate(direction * Time.deltaTime * _moveSpeed);
    }
}
