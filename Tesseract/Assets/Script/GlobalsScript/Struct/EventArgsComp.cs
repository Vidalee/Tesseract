namespace Script.GlobalsScript.Struct
{
    public class EventArgsComp : IEventArgs
    {
        private readonly CompetencesData _comp;
        private readonly int _id;

        public EventArgsComp(CompetencesData comp, int id)
        {
            _comp = comp;
            _id = id;
        }

        public CompetencesData Comp => _comp;

        public int Id => _id;
    }
}
