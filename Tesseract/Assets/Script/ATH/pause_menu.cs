using Script.GlobalsScript;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause_menu : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject CompMenu;
    
    public bool state;
    public int waitTime;
    public bool wait;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !wait)
        {
            state = !state;
            wait = true;
            
            if(state) Active();
            if(!state) Desactive();
        }

        if (wait)
        {
            if (waitTime > 50)
            {
                wait = false;
                waitTime = 0;
            }
            else
            {
                waitTime++;
            }
        }
    }
    
    void Start()
    {
        Canvas.SetActive(false);
    }

    public void Active()
    {
        StaticData.pause = true;
        Canvas.SetActive(true);
        CompMenu.SetActive(false);
        Time.timeScale = 0;
    }

    public void Desactive()
    {
        StaticData.pause = false;
        CompMenu.SetActive(false);
        Canvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadMenu()
    {
        StaticData.Reset();
        GlobalInfo.Reset();
        Time.timeScale = 1;
        StaticData.pause = false;
        
        SceneManager.LoadScene("Login");
    }

    public void ActiveComp()
    {
        Canvas.SetActive(false);
        CompMenu.SetActive(true);
        CompMenu.GetComponent<CompAthAdd>().SetCp();
    }
    
    public void DesactiveComp()
    {
        Canvas.SetActive(true);
        CompMenu.SetActive(false);
    }
}
