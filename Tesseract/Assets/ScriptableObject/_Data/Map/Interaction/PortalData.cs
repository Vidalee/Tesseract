using UnityEngine;

[CreateAssetMenu(fileName = "Portal", menuName = "Map/Portal")]
public class PortalData : ScriptableObject
{
    [SerializeField] protected Vector2[] col;
    [SerializeField] protected AnimationClip animation;
    [SerializeField] protected bool isBoss;
    
    public AnimationClip Animation => animation;

    public Vector2[] Col => col;

    public bool IsBoss => isBoss;
}
