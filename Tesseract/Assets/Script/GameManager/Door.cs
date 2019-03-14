using System.Runtime.CompilerServices;
using UnityEngine;

public class Door : MonoBehaviour
{
    private int cardinal;
    private bool linked;
    private bool open;

    public int GetCardinal() => cardinal;
    public bool GetOpen() => open;
    public bool GetLinked() => linked;
    public void SetLinked(bool linked) => this.linked = linked;

    public void Create(int cardinal)
    {
        this.cardinal = cardinal;
        open = false;
    }   
}
