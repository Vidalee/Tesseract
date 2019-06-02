using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiManager : MonoBehaviour, UDPEventListener
{
    public List<int> playersToAdd = new List<int>();
    public List<int> playersAlreadyInGame = new List<int>();


    private bool ap;
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
            playersToAdd.Add(int.Parse(args[1]));
        }
        if(text == "CPASS")
        {
            socket.Send("JOIN zeoijrzg");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        socket = UDPRoomManager._socket;
        Coffre.Remplir("mode", "multi");
        /*socket = new UDPSocket();

        socket.Client("127.0.0.1", 27000);
        socket.Send("CONNECT Kira kira");*/
        UDPEvent.Register(this);


    }

    // Update is called once per frame
    void Update()
    {
        if (s)
        {
            s = false;
            int[] copy = new int[playersToAdd.Count];
            playersToAdd.CopyTo(copy);
            foreach (int i in copy)
            {
                playersToAdd.Remove(i);
                if (playersAlreadyInGame.Contains(i)) continue;
                playersAlreadyInGame.Add(i);
                Debug.Log("asked to add  " + i);
                MapGridCreation mgc = GameObject.Find("MapManager").GetComponent<MapGridCreation>();
                if (i + "" == (string)Coffre.Regarder("id"))
                    mgc.AddPlayer(i, false);
                else
                {
                    Debug.Log("Adding multi player: " + i);
                    mgc.AddMultiPlayer(i);
                }
            }
        }

    }
}
