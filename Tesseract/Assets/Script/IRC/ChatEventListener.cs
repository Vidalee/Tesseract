using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

interface ChatEventListener 
{
    void OnMessage(string channel, string sender, string message);
    void OnJoin(string channel);
    void OnQuit(string channel);
    void OnPrivateMessage(string user, string message);
}

