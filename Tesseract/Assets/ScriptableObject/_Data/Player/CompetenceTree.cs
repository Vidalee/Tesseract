using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CompetenceTree", menuName = "Player/CompetenceTree")]
public class CompetenceTree : ScriptableObject
{
    [SerializeField] protected CompetencesData[] CompetencesData;
    private Dictionary<string, CompetencesData> _competences;

    private void OnEnable()
    {
        _competences = new Dictionary<string, CompetencesData>();
        foreach (var c in CompetencesData)
        {
            _competences.Add(c.Tag, c);
        }
    }

    public void UnlockCompetence(string tag)
    {
        _competences.TryGetValue(tag, out CompetencesData competence);
        if (competence == null) return;
        competence.Unlock = true;
    }

    public void UpgradeCompetence(CompetencesData competence, float up)
    {
        competence.Speed *= 1 + up;
        competence.Damage += (int) (competence.Damage * up);
        competence.Cooldown *= 1 - up;
    }
}
