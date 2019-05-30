using Script.GlobalsScript;
using UnityEngine;

public class pause_menu : MonoBehaviour
{
    public GameObject Canvas;
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
        Time.timeScale = 0;
    }

    public void Desactive()
    {
        StaticData.pause = false;
        Canvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadMenu()
    {
        StaticData.Reset();
        GlobalInfo.Reset();
        Time.timeScale = 1;
        StaticData.pause = false;
        
        ChangeScene.ChangeToScene("Login");
    }
}
