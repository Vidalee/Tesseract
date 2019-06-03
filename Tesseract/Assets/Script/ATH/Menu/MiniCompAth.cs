using System.Collections;
using Script.GlobalsScript;
using UnityEngine;
using UnityEngine.UI;

public class MiniCompAth : MonoBehaviour
{
    private Text[] Transforms;
    private int index;
    
    private void Awake()
    {
        Transforms = GetComponentsInChildren<Text>();
    }

    public void SetImage(IEventArgs args)
    {
        EventArgsDoubleInt c = (EventArgsDoubleInt) args;
        
        StartCoroutine(SetAth(c.Y, c.X));
    }

    public void LoadAth()
    {
        string s = StaticData.actualData.Name;
        index = 0;
        switch (s)
        {
            case "Archer":
                index = 0;
                break;
            case "Assassin":
                index = 1;
                break;
            case "Mage":
                index = 2;
                break;
            case "Warrior":
                index = 3;
                break;
        }

        CompetencesData[] c = StaticData.actualData.Competences;
        
        Image[] im = GetComponentsInChildren<Image>();

        im[4].sprite = (c[0] as DashComp).Icon1[index];

        if (index == 3)
        {
            im[5].sprite = (c[1] as CacComp).Icon1;
        }
        else
        {
            im[5].sprite = (c[1] as ProjComp).Icon1;
        }
        
        im[6].sprite = (c[2] as ProjComp).Icon1;
        
        im[7].sprite = (c[3] as BoostComp).Icon1[index];
    }
    
    IEnumerator SetAth(int id, int time)
    {
        for (int i = 0; i < time; i++)
        {
            Transforms[id].text = (time - i).ToString();
            yield return new WaitForSeconds(1f);
        }
        Transforms[id].text = "";
    }
}
