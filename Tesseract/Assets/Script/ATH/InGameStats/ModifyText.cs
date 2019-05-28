using Script.GlobalsScript;
using Script.GlobalsScript.Struct;
using UnityEngine;
using UnityEngine.UI;

public class ModifyText : MonoBehaviour
{
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    public void ChangeText(IEventArgs args)
    {
        _text.text = ((EventArgsString) args).X;
    }
}
