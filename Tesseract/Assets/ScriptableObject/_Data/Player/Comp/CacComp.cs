using UnityEngine;

[CreateAssetMenu(fileName = "CompetenceData", menuName = "Competence/Cac")]
public class CacComp : CompetencesData
{
    public int adDamage;
    public int apDamage;

    public override void ChildCreate(CompetencesData competence, int lvl)
    {
        CacComp comp = (CacComp) competence;
        
        adDamage = comp.adDamage + lvl;
        apDamage = comp.apDamage + lvl;
        manaCost = comp.manaCost + 2 * lvl;
    }

    public void UpgradeStats()
    {
        manaCost += 2;
        Lvl++;
        adDamage += 1;
        apDamage += 1;
    }

    public int AdDamage => adDamage;
    
    public int ApDamage => apDamage;
}
