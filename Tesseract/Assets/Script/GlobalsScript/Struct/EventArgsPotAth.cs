using UnityEngine;

namespace Script.GlobalsScript.Struct
{
    public class EventArgsPotAth : IEventArgs
    {
        private readonly Potions _pot;
        private readonly int _id;

        public EventArgsPotAth(Potions pot, int id)
        {
            _pot = pot;
            _id = id;
        }

        public Potions Pot => _pot;

        public int Id => _id;
    }
}
