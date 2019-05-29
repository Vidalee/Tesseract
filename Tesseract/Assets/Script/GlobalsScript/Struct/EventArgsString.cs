namespace Script.GlobalsScript.Struct
{
    public class EventArgsString : IEventArgs
    {
        readonly string _x;

        public EventArgsString(string x)
        {
            _x = x;
        }

        public string X => _x;
    }
}