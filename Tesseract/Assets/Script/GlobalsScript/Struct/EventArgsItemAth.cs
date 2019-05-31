namespace Script.GlobalsScript.Struct
{
    public class EventArgsItemAth : IEventArgs
    {
        private readonly GamesItem _item;

        public EventArgsItemAth(GamesItem item)
        {
            _item = item;
        }

        public GamesItem Item => _item;
    }
}
