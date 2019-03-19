using System;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class UnityEventWithArgs : UnityEvent<GameEventListener>{}

public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;
    [FormerlySerializedAs("Response")] [SerializeField] public UnityEvent<IEventArgs> response;

    private void OnEnable()
    {
        gameEvent.Register(this);
    }

    private void OnDisable()
    {
        gameEvent.UnRegister(this);
    }

    public void OnEventRaised(IEventArgs arg)
    {
        response.Invoke(arg);
    }
}
