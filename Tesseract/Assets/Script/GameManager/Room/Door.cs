using UnityEngine;

public class Door : MonoBehaviour
{
    private int cardinal;
    public Transform closeObj;
    public Transform openObj;
    
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
        Instantiate(openObj, transform.position, Quaternion.identity, transform);
    }
}
