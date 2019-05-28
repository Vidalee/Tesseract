namespace Script.GlobalsScript.Struct
{
    public class EventArgsFloat : IEventArgs
    {
        readonly float _x;

        public EventArgsFloat(float x)
        {
            _x = x;
        }

        public float X => _x;
    }
}