using UnityEngine;

[CreateAssetMenu(fileName = "CompetenceData", menuName = "Competence/Boost")]
public class BoostComp : CompetencesData
{
    [SerializeField] protected int apBoost;
    [SerializeField] protected int adBoost;
    [SerializeField] protected float coolDownBoost;
    [SerializeField] protected int duration;
    [SerializeField] protected Sprite[] icon1;


    public override void ChildCreate(CompetencesData competence, int lvl)
    {
        BoostComp comp = (BoostComp) competence;
        
        apBoost = comp.apBoost + lvl * 5;
        adBoost = comp.adBoost + lvl * 5;
        coolDownBoost = (comp.coolDownBoost + lvl / 2) / 100;
        duration = comp.Duration + lvl;
        manaCost = 0;
        icon1 = comp.icon1;
    }

    public override void UpgradeStats()
    {
        manaCost += 2;
        Lvl += 1;
        apBoost += 1;
        adBoost += 1;
        coolDownBoost += Lvl % 2 == 0 ? 1 : 0;
        duration += 1;
    }

    public int ApBoost => apBoost;

    public int AdBoost => adBoost;

    public float CoolDownBoost => coolDownBoost;

    public int Duration => duration;

    public Sprite[] Icon1 => icon1;
}
