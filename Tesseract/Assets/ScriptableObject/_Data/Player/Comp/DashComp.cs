using UnityEngine;

[CreateAssetMenu(fileName = "CompetenceData", menuName = "Competence/Dash")]
public class DashComp : CompetencesData
{
    [SerializeField] protected float distDash;
    
    public float DistDash => distDash;

    public override void ChildCreate(CompetencesData competence, int lvl)
    {
        distDash = ((DashComp) competence).DistDash;
    }
}
