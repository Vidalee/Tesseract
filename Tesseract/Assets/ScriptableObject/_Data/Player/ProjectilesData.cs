using UnityEngine;

public class ProjectilesData : ScriptableObject
{
    #region Variable

    private Vector3 _direction;
    private int _live;
    private  float _speed;
    private  int _damage;
    private  string _tag;
    private int[] _color;
    private  AnimationClip _anim;

    #endregion

    #region Initialise

    public void Created(Vector3 direction, float speed, int damage, string tag, AnimationClip anim, int live, int[] color)
    {
        _direction = direction;
        _speed = speed;
        _damage = damage;
        _tag = tag;
        _anim = anim;
        _live = live;
        _color = color;
    }

    #endregion

    #region Set/Get

    public Vector3 Direction => _direction;

    public int Live
    {
        get => _live;
        set => _live = value;
    }

    public int[] Color => _color;

    public float Speed => _speed;

    public int Damage => _damage;

    public string Tag => _tag;

    public AnimationClip Anim => _anim;

    #endregion
}
