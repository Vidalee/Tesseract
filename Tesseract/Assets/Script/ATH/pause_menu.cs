using UnityEngine;

public class pause_menu : MonoBehaviour
{
    public GameObject Canvas;

    void Start()
    {
        Canvas.SetActive(false);
    }

    public void Active()
    {
        Canvas.SetActive(true);
        Time.timeScale = 0;
        
    }

    public void Desactive()
    {
        Canvas.SetActive(false);
        Time.timeScale = 1;
    }
}
