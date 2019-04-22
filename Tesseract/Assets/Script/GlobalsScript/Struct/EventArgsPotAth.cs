using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventArgsPotAth : IEventArgs
{
    readonly Sprite _sprite;
    readonly int _x;

    public EventArgsPotAth(int x, Sprite sprite)
    {
        _x = x;
        _sprite = sprite;
    }

    public int X => _x;
    public Sprite Sprite => _sprite;
}
