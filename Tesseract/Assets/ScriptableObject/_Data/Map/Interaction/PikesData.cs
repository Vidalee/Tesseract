using UnityEngine;

[CreateAssetMenu(fileName = "Pikes", menuName = "Map/Pikes")]
public class PikesData : ScriptableObject
{
    [SerializeField] protected int damage;
    [SerializeField] protected Sprite nonTrig;
    [SerializeField] protected Sprite trig;

    public int Damage => damage;

    public Sprite NonTrig => nonTrig;

    public Sprite Trig => trig;
}
