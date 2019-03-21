public class EventArgsCoor : IEventArgs
{
    readonly int _x;
    readonly int _y;

    public EventArgsCoor(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public int X => _x;
    public int Y => _y;
}
