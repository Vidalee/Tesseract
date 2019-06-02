using System.Collections;
using System.Collections.Generic;
using Script.GlobalsScript;
using UnityEngine;

public class EventArgsDoubleInt : IEventArgs
{
    private int x;
    private int y;

    public EventArgsDoubleInt(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int X => x;

    public int Y => y;
}
