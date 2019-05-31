using UnityEngine;

public class ProjectilesData : ScriptableObject
{
    #region Variable

    private Vector3 _direction;
    private int _live;
    private  float _speed;
    private  int _damage;
    private  string _tag;
    private Color _color;
    private  AnimationClip _anim;
    private int _prob;
    private int _effect;
    private int _effectDamage;
    private float _duration;

    #endregion

    #region Initialise

    public void Created(Vector3 direction, float speed, int damage, string tag, AnimationClip anim, int live,
        Color color, int prob, int effect, int effectDamage, float duration)
    {
        _direction = direction;
        _speed = speed;
        _damage = damage;
        _tag = tag;
        _anim = anim;
        _live = live;
        _color = color;
        _prob = prob;
        _effect = effect;
        _effectDamage = effectDamage;
        _duration = duration;
    }

    #endregion

    #region Set/Get

    public Vector3 Direction => _direction;

    public int Live
    {
        get => _live;
        set => _live = value;
    }

    public Color Color => _color;

    public float Speed => _speed;

    public int Damage => _damage;

    public string Tag => _tag;

    public AnimationClip Anim => _anim;

    public int Prob => _prob;

    public int Effect => _effect;

    public int EffectDamage => _effectDamage;

    public float Duration => _duration;

    #endregion
}
