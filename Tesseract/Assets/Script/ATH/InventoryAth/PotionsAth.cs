using Script.GlobalsScript;
using Script.GlobalsScript.Struct;
using UnityEngine;
using UnityEngine.UI;

public class PotionsAth : MonoBehaviour
{
    public Sprite None;
    private Image _sprite;
    private Text _mana;
    private Text _Hp;
    public Potions Pot;

    public GameEvent RemovePotion;

    private void Awake()
    {
        _sprite = transform.GetChild(0).GetComponent<Image>();
        _Hp = transform.GetChild(1).GetComponentInChildren<Text>();
        _mana = transform.GetChild(2).GetComponentInChildren<Text>();
    }

    public void SetPotion(Potions pot)
    {
        if (pot == null)
        {
            _sprite.sprite = None;
            _Hp.text = 0.ToString();
            _mana.text = 0.ToString();
            Pot = null;
            return;
        }

        Pot = pot;
        _sprite.sprite = pot.icon;
        _Hp.text = pot.HpHeal.ToString();
        _mana.text = pot.ManaHeal.ToString();
    }

    public void RemovePot()
    {
        RemovePotion.Raise(new EventArgsInt(int.Parse(name[name.Length - 1] + "")));
    }
}
