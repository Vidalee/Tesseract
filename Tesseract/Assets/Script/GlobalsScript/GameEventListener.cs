using UnityEngine.Events;
using UnityEngine;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;

    private void OnEnable()
    {
        Event.Register(this);
    }

    private void OnDisable()
    {
        Event.UnRegister(this);
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
