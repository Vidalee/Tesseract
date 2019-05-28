using Script.GlobalsScript;
using Script.GlobalsScript.Struct;
using UnityEngine;

public class Modifybar : MonoBehaviour
{
    public void ScaleBar(IEventArgs args)
    {
        transform.localScale = new Vector3(((EventArgsFloat) args).X, transform.localScale.y);
    }
}
