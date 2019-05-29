namespace Script.GlobalsScript.Struct
{
    public class EventArgsItem : IEventArgs
    {
        private readonly GamesItem _item;
    
        public EventArgsItem(GamesItem item)
        {
            _item = item;
        }

        public GamesItem Item => _item;
    }
}