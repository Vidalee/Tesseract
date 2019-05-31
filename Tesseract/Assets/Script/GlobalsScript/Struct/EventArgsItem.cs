using UnityEngine;

namespace Script.GlobalsScript.Struct
{
    public class EventArgsItem : IEventArgs
    {
        private readonly GamesItem _item;
        private readonly Transform _t;
    
        public EventArgsItem(GamesItem item, Transform t)
        {
            _item = item;
            _t = t;
        }

        public GamesItem Item => _item;

        public Transform T => _t;
    }
}