using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesData : ScriptableObject
{
    [SerializeField] protected Vector3 _direction;
    [SerializeField] protected  float _speed;
    [SerializeField] protected  int _damage;
    [SerializeField] protected  string _tag;
    [SerializeField] protected  AnimationClip _anim;

    public void Created(Vector3 direction, float speed, int damage, string tag, AnimationClip anim)
    {
        _direction = direction;
        _speed = speed;
        _damage = damage;
        _tag = tag;
        _anim = anim;
    }

    public Vector3 Direction
    {
        get => _direction;
        set => _direction = value;
    }

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    public int Damage
    {
        get => _damage;
        set => _damage = value;
    }

    public string Tag
    {
        get => _tag;
        set => _tag = value;
    }

    public AnimationClip Anim => _anim;
}
