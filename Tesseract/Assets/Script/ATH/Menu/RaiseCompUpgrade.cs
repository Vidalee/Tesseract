using Script.GlobalsScript.Struct;
using UnityEngine;

public class RaiseCompUpgrade : MonoBehaviour
{
    public GameEvent CompUp;

    public void Upgrade()
    {
        int id = 1;
        
        if (transform.parent.name == "AA") id = 1;
        if (transform.parent.name == "S1") id = 2;
        if (transform.parent.name == "B") id = 3;
        
        CompUp.Raise(new EventArgsInt(id));
    }
}
