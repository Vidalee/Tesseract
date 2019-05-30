using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiManager : MonoBehaviour, UDPEventListener
{
    private bool ap = false;
    public static UDPSocket socket;
    public void OnReceive(string text)
    {

        if (text.StartsWith("SET"))
        {
            string[] args = text.Split(' ');
            if(args[1] == "id")
            {
               
                Debug.Log("pls work");
                ap = true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Coffre.Remplir("mode", "multi");
        socket = new UDPSocket();

        UDPEvent.Register(this);
        socket.Client("127.0.0.1", 27000);
        socket.Send("CONNECT Kira kira");
        
        socket.Send("JOIN zeoijrzg");

    }

    // Update is called once per frame
    void Update()
    {
        if (ap)
        {
            ap = false;
            MapGridCreation mgc = GameObject.Find("MapManager").GetComponent<MapGridCreation>();

            Debug.Log(mgc.seed);
            mgc.AddPlayer(3, false);
            mgc.AddMultiPlayer(2);
        }
    }
}
