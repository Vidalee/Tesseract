using UnityEngine;

[CreateAssetMenu(fileName = "CompetenceData", menuName = "Player/Competence/Dash")]
public class DashComp : CompetencesData
{
    private float distDash;

    public float DistDash => distDash;
}
