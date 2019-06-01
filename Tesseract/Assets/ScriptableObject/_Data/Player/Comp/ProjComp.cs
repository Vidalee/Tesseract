using UnityEngine;

[CreateAssetMenu(fileName = "CompetenceData", menuName = "Player/Competence/Dash")]
public class ProjComp : CompetencesData
{
    [SerializeField] protected int adDamage;
    [SerializeField] protected int apDamage;
    [SerializeField] protected int live;
    [SerializeField] protected int number;
    [SerializeField] protected float speed;
    [SerializeField] protected int lvl;

    public void Created(ProjComp comp, int lvl)
    {
        adDamage = comp.adDamage + lvl;
        apDamage = comp.apDamage + + lvl;
        live = comp.live + + lvl;
        number = comp.number + + lvl;
        speed = comp.speed + + lvl;
        this.lvl = comp.lvl;
    }

    public int AdDamage => adDamage;

    public int ApDamage => apDamage;

    public int Live => live;

    public int Number => number;

    public float Speed => speed;

    public int Lvl => lvl;
}
