﻿using System.Collections.Generic;
using Script.GlobalsScript;
using Script.GlobalsScript.EventScript;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> _eventListeners = new List<GameEventListener>();

    public void Raise(IEventArgs arg)
    {
        for (int i = _eventListeners.Count - 1; i >= 0; i--)
        {
            _eventListeners[i].OnEventRaised(arg);
        }
    }

    public void Register(GameEventListener listener)
    {
        if (!_eventListeners.Contains(listener))
        {
            _eventListeners.Add(listener);
        }
    }

    public void UnRegister(GameEventListener listener)
    {
        if (_eventListeners.Contains(listener))
        {
            _eventListeners.Remove(listener);
        }
    }
}
