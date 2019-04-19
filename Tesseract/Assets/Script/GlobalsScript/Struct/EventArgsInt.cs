public class EventArgsInt : IEventArgs
{
    readonly int _x;

    public EventArgsInt(int x)
    {
        _x = x;
    }

    public int X => _x;
}