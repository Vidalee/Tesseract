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
    private bool _canmove = true;
    private bool _goingTop = false;
    public void Update()
    {
        if (_player == null)
        {
            _player = GameObject.Find("Player(Clone)").transform;
            return;
        }

        if (_canmove)
        {
            Vector3 distanceToPos = _player.position + new Vector3(0, -0.5f) - transform.position;
            float magnitude = distanceToPos.magnitude;
            if (magnitude > _notCloseEnough || (_dashing && magnitude > _attackRange))
            {
                if (!_animationStarted || !_dashing)
                {
                    _a.Play(_player.position.y - 0.5f < transform.position.y ? "PreDash" : "PreDashB");
                    _animationStarted = true;
                    _a.SetBool("Dashing", true);
                    _a.SetBool("Speed", false);
                    _dashing = true;
                    StartCoroutine(Dashing());
                }

                _moveSpeed = _distDash;
            }
            else if (magnitude > _attackRange)
            {
                if (!_animationStarted)
                {
                    _a.SetBool("Speed", true);
                    _a.Play(_player.position.y - 0.5f < transform.position.y ? "Move" : "MoveB");
                    _animationStarted = true;
                }

                StraightToPoint(distanceToPos.normalized);
            }
            else
            {
                _moveSpeed = 2;
                _a.SetBool("Speed", false);
                _a.SetBool("Dashing", false);
                _animationStarted = false;
                _dashing = false;
            }
            
            if (_animationStarted)
            {
                if (_goingTop  != distanceToPos.y > 0)
                {
                    _goingTop = distanceToPos.y > 0;
                    if (_dashing)
                    {
                        _a.Play(_player.position.y - 0.5f < transform.position.y ? "PreDash" : "PreDashB");
                    }
                    else
                    {
                        _a.Play(_player.position.y - 0.5f < transform.position.y ? "Move" : "MoveB");
                    }
                }
            }

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


    public IEnumerator CantMove()
    {
        _canmove = false;
        _animationStarted = false;
        yield return new WaitForSeconds(0.5f);
        _canmove = true;
    }
    private void StraightToPoint(Vector3 direction)
    {
        transform.Translate(direction * Time.deltaTime * _moveSpeed);
    }
}
