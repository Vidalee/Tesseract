using UnityEngine;

[CreateAssetMenu(fileName = "CompetenceData", menuName = "Competence/Boost")]
public class BoostComp : CompetencesData
{
    [SerializeField] protected float apBoost;
    [SerializeField] protected float adBoost;
    [SerializeField] protected float coolDownBoost;
    [SerializeField] protected float manaCostBoost;

    public override void ChildCreate(CompetencesData competence, int lvl)
    {
        BoostComp comp = (BoostComp) competence;
        
        apBoost = comp.apBoost + lvl;
        adBoost = comp.adBoost + lvl;
        coolDownBoost = comp.coolDownBoost + lvl;
        manaCostBoost = comp.manaCostBoost + lvl;
    }
    
    public float ApBoost => apBoost;

    public float AdBoost => adBoost;

    public float CoolDownBoost => coolDownBoost;

    public float ManaCostBoost => manaCostBoost;
}
