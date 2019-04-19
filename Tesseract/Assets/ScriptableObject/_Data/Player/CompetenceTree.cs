using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CompetenceTree", menuName = "Player/CompetenceTree")]
public class CompetenceTree : ScriptableObject
{
    #region Variable

    #endregion

    #region Initialise

    #endregion

    #region Modify

    public void CompetenceLvlUp(CompetencesData[] competence, int lvl)
    {
        foreach (var comp in competence)
        {
            if (comp.Unlock)
            {
                for (int i = 0; i < comp.Upgrade.Length; i++)
                {
                    if (comp.Upgrade[i] == lvl)
                    {
                        UpgradeCompetence(comp, comp.SpeedUpgrade[i], comp.DamageUpgrade[i], comp.CooldownUpgrade[i]);
                    }
                }
            }
            
            if(comp.UnlockLvl == lvl) UnlockCompetence(comp);
        }
    }

    private void UnlockCompetence(CompetencesData competence)
    {
        competence.Unlock = true;
    }

    private void UpgradeCompetence(CompetencesData competence, float speed, float damage, float cooldown)
    {
        competence.Speed *= 1 + speed;
        competence.Damage += (int) (competence.Damage * damage);
        competence.Cooldown *= 1 - cooldown;
    }

    #endregion
}
