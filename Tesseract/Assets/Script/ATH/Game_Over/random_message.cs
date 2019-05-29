using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class random_message : MonoBehaviour
{
    public Text message;
    private int t;  
    private string[] Str_Array = new[]
        {"Despite your efforts, the Dungeon overcame you... ", "Take up arms again for the Tesseract", "It wasn't the right strategy...", "Oops..."};
   
    // Start is called before the first frame update
    void Start()
    {
        t = Random.Range(0,Str_Array.Length);

    }

    // Update is called once per frame
    void Update()
    {
        message.text = Str_Array[t]; 
    }
}
