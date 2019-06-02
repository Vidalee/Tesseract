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
    }

    public int AdDamage => adDamage;
    
    public int ApDamage => apDamage;
}
