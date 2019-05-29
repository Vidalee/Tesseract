using UnityEngine;

[CreateAssetMenu(fileName = "Portal", menuName = "Map/Portal")]
public class PortalData : ScriptableObject
{
    [SerializeField] protected Vector2[] col;
    [SerializeField] protected AnimationClip _animation;

    public AnimationClip Animation => _animation;

    public Vector2[] Col => col;
}
