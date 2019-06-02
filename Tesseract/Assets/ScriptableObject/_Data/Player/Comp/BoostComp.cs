using UnityEngine;

[CreateAssetMenu(fileName = "CompetenceData", menuName = "Competence/Boost")]
public class BoostComp : CompetencesData
{
    [SerializeField] protected int apBoost;
    [SerializeField] protected int adBoost;
    [SerializeField] protected float coolDownBoost;
    [SerializeField] protected int duration;

    public override void ChildCreate(CompetencesData competence, int lvl)
    {
        BoostComp comp = (BoostComp) competence;
        
        apBoost = comp.apBoost + lvl * 5;
        adBoost = comp.adBoost + lvl * 5;
        coolDownBoost = (comp.coolDownBoost + lvl / 2) / 100;
        duration = comp.Duration;
    }
    
    public int ApBoost => apBoost;

    public int AdBoost => adBoost;

    public float CoolDownBoost => coolDownBoost;

    public int Duration => duration;
}
