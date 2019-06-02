using UnityEngine;

[CreateAssetMenu(fileName = "CompetenceData", menuName = "Competence/Dash")]
public class DashComp : CompetencesData
{
    [SerializeField] protected float distDash;
    [SerializeField] protected Sprite[] icon1;
    
    public float DistDash => distDash;

    public override void ChildCreate(CompetencesData competence, int lvl)
    {
        distDash = ((DashComp) competence).DistDash;
        icon1 = ((DashComp) competence).icon1;
    }

    public override void UpgradeStats()
    {
    }

    public Sprite[] Icon1 => icon1;
}
