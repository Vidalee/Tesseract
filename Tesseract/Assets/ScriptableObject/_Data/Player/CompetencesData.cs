using UnityEngine;

[CreateAssetMenu(fileName = "CompetenceData", menuName = "Player/Competence")]
public class CompetencesData : ScriptableObject
{
    #region Variable

    [SerializeField] protected string _Name;
    [SerializeField] protected bool _Unlock;
    [SerializeField] protected int _UnlockLvl;
    [SerializeField] protected float _Cooldown;
    [SerializeField] protected int[] _Upgrade;
    [SerializeField] protected int[] _DamageUpgrade;
    [SerializeField] protected int[] _SpeedUpgrade;
    [SerializeField] protected int[] _CooldownUpgrade;
    [SerializeField] protected int manaCost;
    private bool _Usable;

    [SerializeField] protected float _Speed;
    [SerializeField] protected int _Damage;
    [SerializeField] protected int live;
    [SerializeField] protected int number;
    [SerializeField] protected int[] color;
    
    [SerializeField] protected Transform _Object;
    [SerializeField] protected string _Tag;
    [SerializeField] protected AnimationClip _AnimationClip;

    #endregion

    #region Set/Get

    private void OnEnable()
    {
        _Usable = _Unlock;
        if (_Upgrade.Length == 0)
        {
            _Upgrade = new[] {1, 5, 10, 20};
        }

        if (_DamageUpgrade.Length == 0)
        {
            _DamageUpgrade = new[] {10, 10, 10, 10};
        }

        if (_SpeedUpgrade.Length == 0)
        {
            _SpeedUpgrade = new[] {5, 5, 5, 5};
        }

        if (_CooldownUpgrade.Length == 0)
        {
            _CooldownUpgrade = new[] {5, 5, 5, 5};
        }
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

    public int Live
    {
        get => live;
        set => live = value;
    }

    public int UnlockLvl => _UnlockLvl;

    public int[] Upgrade => _Upgrade;

    public int[] Color => color;

    public int ManaCost
    {
        get => manaCost;
        set => manaCost = value;
    }

    public int Number
    {
        get => number;
        set => number = value;
    }

    public string Name => _Name;

    public AnimationClip AnimationClip => _AnimationClip;

    public Transform Object => _Object;

    public string Tag => _Tag;

    public int[] DamageUpgrade => _DamageUpgrade;

    public int[] SpeedUpgrade => _SpeedUpgrade;

    public int[] CooldownUpgrade => _CooldownUpgrade;

    #endregion
}
