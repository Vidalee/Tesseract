using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiManager : MonoBehaviour, UDPEventListener
{
    private bool ap = false;
    private bool ad = true;
    private bool s = false;
    private int next = 0;
    public static UDPSocket socket;
    
    public void OnReceive(string text)
    {
        string[] args = text.Split(' ');

        if (text.StartsWith("SET"))
        {
            if(args[1] == "id")
            {
               
                Debug.Log("pls work");
                ap = true;
            }
        }if(args[0] == "SPAWN")
        {
            s = true;
            next = int.Parse(args[1]);
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
        if (ap && ad)
        {
            ad = false;
            ap = false;
            MapGridCreation mgc = GameObject.Find("MapManager").GetComponent<MapGridCreation>();

            Debug.Log(mgc.seed);
            mgc.AddPlayer(int.Parse((string) Coffre.Regarder("id")), false);
        }

        if (s)
        {
            s = false;
            MapGridCreation mgc = GameObject.Find("MapManager").GetComponent<MapGridCreation>();
            mgc.AddMultiPlayer(3);

        }
    }
}
