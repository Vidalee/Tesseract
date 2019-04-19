public static class CompetenceTree
{
    #region Variable

    #endregion

    #region Initialise

    #endregion

    #region Modify

    public static void CompetenceLvlUp(CompetencesData[] competence, int lvl)
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

    private static void UnlockCompetence(CompetencesData competence)
    {
        competence.Unlock = true;
    }

    private static void UpgradeCompetence(CompetencesData competence, float speed, float damage, float cooldown)
    {
        competence.Speed *= 1 + speed/100;
        competence.Damage = (int) (competence.Damage * (1 + damage/100));
        competence.Cooldown *= 1 - cooldown/100;
    }

    #endregion
}
