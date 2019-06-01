using UnityEngine;

[CreateAssetMenu(fileName = "CompetenceData", menuName = "Player/Competence/Dash")]
public class CacComp : CompetencesData
{
    public int adDamage;
    public int apDamage;

    public void Created(CacComp comp, int lvl)
    {
        adDamage = comp.adDamage + lvl;
        apDamage = comp.apDamage + lvl;
    }
    
    public int AdDamage => adDamage;
    public int ApDamage => apDamage;
}
