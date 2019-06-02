using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CompetenceData", menuName = "Competence/Proj")]
public class ProjComp : CompetencesData
{
    [SerializeField] protected int adDamage;
    [SerializeField] protected int apDamage;
    [SerializeField] protected int live;
    [SerializeField] protected int number;
    [SerializeField] protected float speed;
    [SerializeField] protected float addNumber;
    [SerializeField] protected float AddLive;

    public override void ChildCreate(CompetencesData competence, int lvl)
    {
        ProjComp comp = (ProjComp) competence;

        manaCost = comp.manaCost + lvl;
        addNumber = Math.Abs(addNumber) < 0.01 ? 0 : 1 / comp.addNumber;
        adDamage = comp.adDamage + lvl;
        apDamage = comp.apDamage + lvl;
        live = comp.live + (int) (AddLive * lvl);
        number = comp.number + (int)(addNumber * lvl);
        speed = comp.speed;
    }

    public void UpgradeStats()
    {
        manaCost++;
        Lvl++;
        adDamage++;
        apDamage++;
        live += Lvl % (int) AddLive == 0 ? 1 : 0;
        number += Lvl % (int) addNumber == 0 ? 1 : 0;
    }
    
    public int AdDamage => adDamage;

    public int ApDamage => apDamage;

    public int Live => live;

    public int Number => number;

    public float Speed => speed;
}
