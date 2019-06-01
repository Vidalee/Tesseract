namespace Script.GlobalsScript.Struct
{
    public class EventArgsCoor : IEventArgs
    {
        readonly int _x;
        readonly int _y;
        readonly int _id;

        public EventArgsCoor(int x, int y, int id)
        {
            _x = x;
            _y = y;
            _id = id;
        }

        public int X => _x;
        public int Y => _y;
        public int Id => _id;
    }
}