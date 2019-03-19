public struct IntEventArgs : IEventArgs
{
    public readonly int Int;

    public IntEventArgs(int i)
    {
        Int = i;
    }
}
