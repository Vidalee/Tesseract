using UnityEngine;
using UnityEngine.UI;

public class CompSetAthMenuLoad : MonoBehaviour
{
    public void Create(CompetencesData comp, int index)
    {
        switch (comp)
        {
            case BoostComp compe:
                BoostCompAth(compe, index);
                    break;
            case ProjComp compe:
                ProjCompAth(compe);
                    break;
            case CacComp compe:
                CacCompAth(compe);
                    break;
        }
    }

    public void CacCompAth(CacComp comp)
    {
        GetComponentInChildren<Image>().sprite = comp.Icon1;
        Text[] te = transform.GetComponentsInChildren<Text>();

        te[0].text = comp.Name;
        te[2].text = "Cooldown : " + comp.Cooldown;
        te[3].text = "Damage : " + comp.AdDamage;
        te[4].text = "Damage : " + comp.ApDamage;
        te[5].text = "Mana Cost : " + comp.ManaCost;
    }
    
    public void ProjCompAth(ProjComp comp)
    {
        GetComponentInChildren<Image>().sprite = comp.Icon1;
        
        Text[] te = transform.GetComponentsInChildren<Text>();
        te[0].text = comp.Name;
        te[2].text = "Cooldown : " + comp.Cooldown;
        te[3].text = "Damage : " + comp.AdDamage;
        te[4].text = "Damage : " + comp.ApDamage;
        te[5].text = "Mana Cost : " + comp.ManaCost;
        te[6].text = "Number : " + comp.Number;
        te[7].text = "Perforant : " + comp.Live;
    }
    
    public void BoostCompAth(BoostComp comp, int index)
    {
        GetComponentInChildren<Image>().sprite = comp.Icon1[index];
                
        Text[] te = transform.GetComponentsInChildren<Text>();
        te[0].text = comp.Name;
        te[2].text = "Cooldown : " + comp.Cooldown;
        te[4].text = "Mana Cost : " + comp.ManaCost;
        te[3].text = "Damage Boost : " + comp.AdBoost;
        te[5].text = "Damage Boost : " + comp.ApBoost;
        Debug.Log(comp.ApBoost);
    }
}
