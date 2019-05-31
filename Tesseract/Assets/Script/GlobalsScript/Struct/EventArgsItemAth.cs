namespace Script.GlobalsScript.Struct
{
    public class eventArgsItemAth : IEventArgs
    {
        private readonly GamesItem _item;

        public eventArgsItemAth(GamesItem item)
        {
            _item = item;
        }

        public GamesItem Item => _item;
    }
}
