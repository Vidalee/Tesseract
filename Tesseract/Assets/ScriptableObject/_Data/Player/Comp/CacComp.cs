using UnityEngine;

[CreateAssetMenu(fileName = "CompetenceData", menuName = "Competence/Cac")]
public class CacComp : CompetencesData
{
    public int adDamage;
    public int apDamage;
    [SerializeField] protected Sprite icon1;

    public override void ChildCreate(CompetencesData competence, int lvl)
    {
        CacComp comp = (CacComp) competence;
        
        adDamage = comp.adDamage + lvl;
        apDamage = comp.apDamage + lvl;
        manaCost = comp.manaCost + 2 * lvl;
        icon1 = comp.icon1;
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

    public Sprite Icon1 => icon1;
}
