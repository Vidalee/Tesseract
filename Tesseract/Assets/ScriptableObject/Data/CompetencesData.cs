using UnityEngine;

[CreateAssetMenu(fileName = "CompetenceData", menuName = "Competence/Data")]
public class CompetencesData : ScriptableObject
{
    [SerializeField] protected string _Name;
    [SerializeField] protected bool _Unlock;
    [SerializeField] protected float _Cooldown;
    private bool _Usable;
    [SerializeField] protected Sprite _Sprite;
    [SerializeField] protected AnimationClip _AnimationClip;
    [SerializeField] protected float _Speed;
    [SerializeField] protected int _Damage;
    [SerializeField] protected Transform _Object;
    [SerializeField] protected string _Tag;
    [SerializeField] protected float _SpriteSize;

    private void OnEnable()
    {
        _Usable = _Unlock;
    }

    public float Cooldown
    {
        get => _Cooldown;
        set => _Cooldown = value;
    }

    public bool Unlock
    {
        get => _Unlock;
        set => _Unlock = value;
    }

    public bool Usable
    {
        get => _Usable;
        set => _Usable = value;
    }

    public float Speed
    {
        get => _Speed;
        set => _Speed = value;
    }

    public int Damage
    {
        get => _Damage;
        set => _Damage = value;
    }

    public string Name => _Name;

    public Sprite Sprite => _Sprite;

    public AnimationClip AnimationClip => _AnimationClip;

    public Transform Object => _Object;

    public string Tag => _Tag;

    public float SpriteSize => _SpriteSize;
}
