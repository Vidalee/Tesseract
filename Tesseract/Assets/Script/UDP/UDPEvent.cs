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
            try
            {
                _eventListeners[i].OnReceive(text);
            }
            catch
            {
                Debug.Log("TCP PACKED ERROR");
            }
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

