using UnityEngine;

public static class AnimatorOverride
{
    public static void AnimationOverride(string name, AnimationClip[] anim, AnimatorOverrideController aoc, Animator ac)
    {
        ac.runtimeAnimatorController = aoc;

        string[] s = {"L", "T", "R", "B"};
        
        for (int i = 0; i < 4; i++)
        {
            aoc[name + s[i]] = anim[i];
        }
    }

    public static void EnemyAnimationOverride(string name, AnimationClip[] anim, AnimatorOverrideController aoc, Animator ac)
    {
        ac.runtimeAnimatorController = aoc;

        string[] s = {"L", "R"};
        
        for (int i = 0; i < 2; i++)
        {
            aoc[name + s[i]] = anim[i];
        }
    }    

    public static void AnimationOverride(string name, AnimationClip anim, AnimatorOverrideController aoc, Animator ac)
    {
        ac.runtimeAnimatorController = aoc;
        aoc[name] = anim;
    }
}