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

    private Transform _player;
    [SerializeField] protected LayerMask entityLayer;

    private bool _dashing = false;
    private bool _animationStarted = false;
    public void Update()
    {
        if (_player == null)
        {
            _player = GameObject.Find("Player(Clone)").transform;
            return;
        }
        
        Vector3 distanceToPos = _player.position + new Vector3(0, -0.5f) - transform.position;
        float magnitude = distanceToPos.magnitude;
        if (magnitude > _notCloseEnough || (_dashing && magnitude > _attackRange))
        {
            if (!_animationStarted || !_dashing)
            {
                _a.Play("PreDash");
                _animationStarted = true;
                _a.SetBool("Dashing", true);
                _a.SetBool("Speed", false);
                _dashing = true;
                StartCoroutine(Dashing());
            }
            _moveSpeed = _distDash;
            //RaycastHit2D linecast = Physics2D.Linecast(transform.position, _player.position, entityLayer);
            //Debug.Log(linecast.point);
            //StartCoroutine(Dash((_player.position - transform.position).normalized));
        }
        else if (magnitude > _attackRange)
        {
            if (!_animationStarted)
            {
                _a.SetBool("Speed", true);
                _a.Play("Move");
                _animationStarted = true;
            }
            StraightToPoint(distanceToPos.normalized);
        }
        else
        {
            _moveSpeed = 1;
            _a.SetBool("Speed", false);
            _a.SetBool("Dashing", false);
            _animationStarted = false;
            _dashing = false;
        }
    }

    public IEnumerator Dashing()
    {
        yield return new WaitForSeconds(0.5f);
        while (_dashing)
        {
            Vector3 pos = _player.position + new Vector3(0, -0.5f) - transform.position;
            StraightToPoint(pos.normalized);
            yield return null;
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
