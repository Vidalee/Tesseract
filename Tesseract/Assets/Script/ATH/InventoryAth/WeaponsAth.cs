using Script.GlobalsScript.Struct;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsAth : MonoBehaviour
{
    public Sprite None;
    private Image _sprite;
    private Text _ad;
    private Text _ap;
    private Text _cd;
    private Image _effectI;
    private Text _effectT;
    public Weapons Weapons;

    public GameEvent RemoveWeapons;

    private void Awake()
    {
        _sprite = transform.GetChild(0).GetComponent<Image>();
        _ad = transform.GetChild(1).GetComponentInChildren<Text>();
        _ap = transform.GetChild(2).GetComponentInChildren<Text>();
        _cd = transform.GetChild(3).GetComponentInChildren<Text>();
        _effectT = transform.GetChild(4).GetComponentInChildren<Text>();
        _effectI = transform.GetChild(4).GetComponent<Image>();
    }
    
    public void SetWeapons(Weapons weapons)
    {
        if (weapons == null)
        {
            _sprite.sprite = None;
            _ad.text = 0.ToString();
            _ap.text = 0.ToString();
            _cd.text = 0 + "%";
            _effectT.text = "";
            _effectI.sprite = None;
            Weapons = null;
            return;
        }

        Weapons = weapons;
        _sprite.sprite = weapons.icon;
        _ad.text = weapons.PhysicsDamage.ToString();
        _ap.text = weapons.MagicDamage.ToString();
        _cd.text = weapons.Cd + "%";
        _effectT.text = weapons.EffectDamage == 0 ? "" : weapons.EffectDamage.ToString();
        _effectI.sprite = weapons.EffectSprite;
    }

    public void RemoveWeapon()
    {
        RemoveWeapons.Raise(new EventArgsNull());
    }
}
