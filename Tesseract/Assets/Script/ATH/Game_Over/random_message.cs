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
        {
            "Despite your efforts, the Dungeon overcame you... ", 
            "Take up arms again for the Tesseract", 
            "It wasn't the right strategy...", 
            "Oops...",
            "Oops you did it again !",
            "Heroes never d... Oh wait...",
            "Deja vu ! You've just been in this place before !",
             "You s*ck boi",
            "You better stop here and go outside"
        };
   
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
