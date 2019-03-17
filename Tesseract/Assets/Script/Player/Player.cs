using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected PlayerData PlayerData;
    [SerializeField] protected AnimatorOverrideController Aoc;

    private void Awake()
    {
        PlayerData.Hp = PlayerData.MaxHp;
        foreach (var c in PlayerData.Competences)
        {
            c.Usable = c.Unlock;
        }
        AnimationOverride("DefaultMove", PlayerData.Move);
        AnimationOverride("DefaultThrow", PlayerData.Throw);
        AnimationOverride("DefaultIdle", PlayerData.Idle);
    }
        
    public void AnimationOverride(string name, AnimationClip[] anim)
    {
        Animator ac = GetComponent<Animator>();
        ac.runtimeAnimatorController = Aoc;

        string[] s = {"L", "T", "R", "B"};
        
        for (int i = 0; i < 4; i++)
        {
            Aoc[name + s[i]] = anim[i];
        }
    }
}
