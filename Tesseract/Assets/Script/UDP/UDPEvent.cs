using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class UDPEvent
{
    private static List<UDPEventListener> _eventListeners = new List<UDPEventListener>();

    public static void Receive(string text)
    {
        for (int i = _eventListeners.Count - 1; i >= 0; i--)
        {
            _eventListeners[i].OnReceive(text);
        }
    }

    public static void Register(UDPEventListener listener)
    {
        if (!_eventListeners.Contains(listener))
        {
            _eventListeners.Add(listener);
        }
    }

    public static void UnRegister(UDPEventListener listener)
    {
        if (_eventListeners.Contains(listener))
        {
            _eventListeners.Remove(listener);
        }
    }
}

