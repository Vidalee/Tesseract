using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class ChatEvent : ScriptableObject
{
    private static List<ChatEventListener> _eventListeners = new List<ChatEventListener>();

    public static void Message(string channel, string sender, string message)
    {
        for (int i = _eventListeners.Count - 1; i >= 0; i--)
        {
            _eventListeners[i].OnMessage(channel, sender, message);
        }
    }

    public static void Join(string channel)
    {
        for (int i = _eventListeners.Count - 1; i >= 0; i--)
        {
            _eventListeners[i].OnJoin(channel);
        }
    }

    public static void PrivateMessage(string user, string message)
    {
        for (int i = _eventListeners.Count - 1; i >= 0; i--)
        {
            _eventListeners[i].OnPrivateMessage(user, message);
        }
    }

    public static void Quit(string channel)
    {
        for (int i = _eventListeners.Count - 1; i >= 0; i--)
        {
            _eventListeners[i].OnQuit(channel);
        }
    }

    public static void Register(ChatEventListener listener)
    {
        if (!_eventListeners.Contains(listener))
        {
            _eventListeners.Add(listener);
        }
    }

    public static void UnRegister(ChatEventListener listener)
    {
        if (_eventListeners.Contains(listener))
        {
            _eventListeners.Remove(listener);
        }
    }
}

