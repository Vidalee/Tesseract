using UnityEngine;

[CreateAssetMenu(fileName = "CompetenceData", menuName = "Competence/Boost")]
public class BoostComp : CompetencesData
{
    [SerializeField] protected int apBoost;
    [SerializeField] protected int adBoost;
    [SerializeField] protected int coolDownBoost;

    public override void ChildCreate(CompetencesData competence, int lvl)
    {
        BoostComp comp = (BoostComp) competence;
        
        apBoost = comp.apBoost + lvl;
        adBoost = comp.adBoost + lvl;
        coolDownBoost = comp.coolDownBoost + lvl;
    }
    
    public int ApBoost => apBoost;

    public int AdBoost => adBoost;

    public int CoolDownBoost => coolDownBoost;
}
