using System;
using UnityEngine;
using UnityEngine.Events;

namespace Script.GlobalsScript.EventScript
{
    [Serializable]
    public class UnityEventWithArgs : UnityEvent<IEventArgs>{}

    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        [SerializeField] public UnityEventWithArgs response;

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
}