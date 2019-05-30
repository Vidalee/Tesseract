using Script.GlobalsScript;
using UnityEngine;
using UnityEngine.UI;

public class CreateLvlMapMenu : MonoBehaviour
{
    public Transform lvl;
    
    private string[] text;
    
    void Start()
    {
        text = new[]
        {
            "1-5", "5-10", "10-15", "15-20", "20-25", "25-30","30-35","35-40","40-45","45-50","50-60","60-70","70-80","80-90","90-100"
        };
        
        GenerateBar();
    }

    private void GenerateBar()
    {
        double number = 1;
        GlobalSave save = SaveSystem.LoadGlobal();
        if (save == null)
        {
            GlobalInfo.MaxLvl = 1;
            SaveSystem.SaveGlobal();
        }
        else
        {
            number = save.maxLvl == 0 ? 1 : save.maxLvl;
        }

        Vector3 init = new Vector3(-600, 330, 0);
        
        for (int i = 0; i < number; i++)
        {
            if (i > 4)
            {
                init = new Vector3(0, 330, 0);
            }

            if (i > 9)
            {
                init = new Vector3(600, 330, 0);
            }
            
            Transform o = Instantiate(lvl, init + new Vector3(0, -150 * (i % 5), 0), Quaternion.identity, transform);

            o.name = text[i];
        }
    }
}
