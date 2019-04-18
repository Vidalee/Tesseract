using UnityEngine;

[CreateAssetMenu(fileName = "GeneralScript", menuName = "GeneralScript/Animation/AnimatorOverride")]
public class AnimatorOverride : ScriptableObject
{
    public void AnimationOverride(string name, AnimationClip[] anim, AnimatorOverrideController aoc, Animator ac)
    {
        ac.runtimeAnimatorController = aoc;

        string[] s = {"L", "T", "R", "B"};
        
        for (int i = 0; i < 4; i++)
        {
            aoc[name + s[i]] = anim[i];
        }
    }

    public void AnimationOverride(string name, AnimationClip anim, AnimatorOverrideController aoc, Animator ac)
    {
        ac.runtimeAnimatorController = aoc;
        aoc[name] = anim;
    }
}