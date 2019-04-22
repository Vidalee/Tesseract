using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SplashPotions", menuName = "Items/GameItems/SplashPotions")]
public class SplashPotions : ScriptableObject
{
    [SerializeField] protected string type;
    [SerializeField] protected CompetencesData competence;
    
    public string Type => type;

    public CompetencesData Competence => competence;
}
