using UnityEngine;

[CreateAssetMenu(fileName = "CompetenceData", menuName = "Competence/Proj")]
public class ProjComp : CompetencesData
{
    [SerializeField] protected int adDamage;
    [SerializeField] protected int apDamage;
    [SerializeField] protected int live;
    [SerializeField] protected int number;
    [SerializeField] protected float speed;

    public override void ChildCreate(CompetencesData competence, int lvl)
    {
        ProjComp comp = (ProjComp) competence;
        
        adDamage = comp.adDamage + lvl;
        apDamage = comp.apDamage + + lvl;
        live = comp.live + + lvl;
        number = comp.number + + lvl;
        speed = comp.speed + + lvl;
    }
    
    public int AdDamage => adDamage;

    public int ApDamage => apDamage;

    public int Live => live;

    public int Number => number;

    public float Speed => speed;
}
