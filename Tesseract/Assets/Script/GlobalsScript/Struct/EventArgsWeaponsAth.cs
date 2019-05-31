namespace Script.GlobalsScript.Struct
{
    public class EventArgsWeaponsAth : IEventArgs
    {
        private readonly Weapons _weapons;

        public EventArgsWeaponsAth(Weapons weapons)
        {
            _weapons = weapons;
        }

        public Weapons Weapons => _weapons;
    }
}
