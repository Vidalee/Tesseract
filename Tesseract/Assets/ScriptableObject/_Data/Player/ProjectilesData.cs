using System.Collections.Generic;
using UnityEngine;

public class ProjectilesData : ScriptableObject
{
    private Vector3 _direction;
    private int _live;
    private  float _speed;
    private  int _damage;
    private  string _tag;
    private  AnimationClip _anim;

    public void Created(Vector3 direction, float speed, int damage, string tag, AnimationClip anim, int live)
    {
        _direction = direction;
        _speed = speed;
        _damage = damage;
        _tag = tag;
        _anim = anim;
        _live = live;
    }

    public Vector3 Direction => _direction;

    public int Live
    {
        get => _live;
        set => _live = value;
    }

    public float Speed => _speed;

    public int Damage => _damage;

    public string Tag => _tag;

    public AnimationClip Anim => _anim;
}
