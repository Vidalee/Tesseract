public struct IntEventArgs : IEventArgs
{
    private readonly int Int;

    public IntEventArgs(int i)
    {
        Int = i;
    }
}
