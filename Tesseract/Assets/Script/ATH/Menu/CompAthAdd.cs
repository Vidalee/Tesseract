using Script.GlobalsScript;
using Script.GlobalsScript.Struct;
using UnityEngine;
using UnityEngine.UI;

public class CompAthAdd : MonoBehaviour
{

    public Transform AAC;
    public Transform Spell;
    public Transform Boost;
    public Text Cp;

    Transform a;
    Transform b;
    Transform c;
    
    public void SetAth()
    {
        CompetencesData[] Comp = StaticData.actualData.Competences;

        Transform t = transform.GetChild(0);
        Cp = transform.GetChild(1).GetComponentInChildren<Text>();
        Cp.text = "Competence Point : " + StaticData.actualData.CompPoint;

        b = Instantiate(Spell, Vector3.zero, Quaternion.identity, t);
        b.GetComponent<CompSetAthMenuLoad>().Create(Comp[2], 2);

        if (StaticData.actualData.Name == "Warrior")
        {
            a = Instantiate(AAC, Vector3.zero, Quaternion.identity, t);
            a.GetComponent<CompSetAthMenuLoad>().Create(Comp[1], 1);
        }
        else
        {
            a = Instantiate(Spell, Vector3.zero, Quaternion.identity, t);
            a.GetComponent<CompSetAthMenuLoad>().Create(Comp[1], 1);
        }
        
        c = Instantiate(Boost, Vector3.zero, Quaternion.identity, t);
        c.GetComponent<CompSetAthMenuLoad>().Create(Comp[3], 3);

        a.name = "AA";
        b.name = "S1";
        c.name = "B";
        
        gameObject.SetActive(false);
    }

    public void SetCp()
    {
        Cp.text = "Competence Point : " + StaticData.actualData.CompPoint;
    }
    
    public void UpdateAth(IEventArgs comp)
    {
        SetCp();
        
        EventArgsComp cc = (EventArgsComp) comp;
        int index = cc.Id;
        CompetencesData co = cc.Comp;
        
        if (co == null) return;

        
        if(index == 1)
        {
            if (StaticData.actualData.Name == "Warrior")
                a.GetComponent<CompSetAthMenuLoad>().Create(co, 1);
            else 
                a.GetComponent<CompSetAthMenuLoad>().Create(co, 1);
        }
        else if (index == 2)
        {
            b.GetComponent<CompSetAthMenuLoad>().Create(co, 2);
        }
        else if (index == 3)
        {
            c.GetComponent<CompSetAthMenuLoad>().Create(co, 3);
        }
    }
}
