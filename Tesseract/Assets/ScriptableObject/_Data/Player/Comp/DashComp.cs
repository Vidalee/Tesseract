using UnityEngine;

[CreateAssetMenu(fileName = "CompetenceData", menuName = "Competence/Dash")]
public class DashComp : CompetencesData
{
    private float distDash;

    public float DistDash => distDash;

    public override void ChildCreate(CompetencesData competence, int lvl)
    {
        distDash = ((DashComp) competence).DistDash;
    }
}
