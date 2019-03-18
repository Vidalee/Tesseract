using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> _eventListeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = _eventListeners.Count - 1; i >= 0; i--)
        {
            _eventListeners[i].OnEventRaised();
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
