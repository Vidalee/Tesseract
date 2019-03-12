using UnityEngine;

public class Door : MonoBehaviour
{
    private int cardinal;
    public Transform CloseT;
    public Transform CloseB;
    public Transform OpenT;
    public Transform OpenB;
    public float scale;
    
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
        Instantiate(CloseT, transform.position + new Vector3(0,scale,0), Quaternion.identity, transform);
        Instantiate(CloseB, transform.position, Quaternion.identity, transform);
    }
}
