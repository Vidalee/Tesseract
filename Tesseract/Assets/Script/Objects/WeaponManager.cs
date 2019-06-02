using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Script.GlobalsScript;
using Script.GlobalsScript.Struct;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] protected Weapons Weapon;
    [SerializeField] private SpriteRenderer _sprite;

    public GameEvent PlayerMoveEvent;
    public bool isAttacking;

    public GameEvent AthItem;
    public GameEvent AthItemS;
    public GameEvent AddItem;
    public bool wait;

    private PlayerData _playerData;
    private CacComp comp;
    
    public void Create(Weapons weapon)
    {
        _playerData = StaticData.actualData;
        comp = _playerData.Name == "Warrior" ? _playerData.Competences[1] as CacComp : null;
        
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = weapon.icon;
        if (!weapon.inPlayerInventory) _sprite.sortingOrder = (int) ((transform.position.y + 0.7) * -10);
        Weapon = weapon;
        EdgeCollider2D collider = GetComponent<EdgeCollider2D>();
        collider.points = weapon.ColliderPoints;
                
        PlayerMoveEvent.Raise(new EventArgsCoor(0, -1, int.Parse((string) Coffre.Regarder("id"))));
    }
    
    public void AttackWithWeapon(IEventArgs args)
    {
        if (Weapon.inPlayerInventory)
        {
            if (Weapon._class == "Warrior" || Weapon._class == "Archer")
            {
                Vector3 move = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                bool dir = Math.Abs(move.x) < Math.Abs(move.y);
                if (dir)
                {
                    if (move.y < 0)
                    {
                        StartCoroutine(Attack("down"));
                    }
                    else
                    {
                        StartCoroutine(Attack("up"));
                    }
                }
                else
                {
                    if (move.x < 0)
                    {
                        StartCoroutine(Attack("left"));

                    }
                    else
                    {
                        StartCoroutine(Attack("right"));
                    }
                }
            }
        }
    }
    
    IEnumerator Attack(string dir)
    {
        if (Weapon._class == "Warrior")
        {
            isAttacking = true;
            WaitForSeconds frame = new WaitForSeconds(0.008f);

            switch (dir)
            {
                case "right":
                    PlaceWeapon(0.314f, 0.258f, 90);
                    yield return frame;
                    PlaceWeapon(0.349f, 0.229f, 70);
                    yield return frame;
                    PlaceWeapon(0.357f, 0.216f, 50);
                    yield return frame;
                    PlaceWeapon(0.357f, 0.216f, 30);
                    yield return frame;
                    PlaceWeapon(0.336f, 0.171f, 10);
                    yield return frame;
                    PlaceWeapon(0.299f, 0.131f, -10);
                    yield return frame;
                    PlaceWeapon(0.264f, 0.076f, -30);
                    yield return frame;
                    PlaceWeapon(0.244f, 0.07f, -50);
                    yield return frame;
                    PlaceWeapon(0.209f, 0.048f, -70);
                    yield return frame;
                    PlaceWeapon(0.165f, 0.018f, -90);
                    yield return frame;
                    break;
                case "down":
                    _sprite.sortingOrder = 1000;
                    PlaceWeapon(-0.004f, -0.011f, 26);
                    yield return frame;
                    PlaceWeapon(0.082f, -0.021f, 2);
                    yield return frame;
                    PlaceWeapon(0.12f, -0.049f, -22);
                    yield return frame;
                    PlaceWeapon(0.106f, -0.097f, -46);
                    yield return frame;
                    PlaceWeapon(0.072f, -0.124f, -70);
                    yield return frame;
                    PlaceWeapon(0.012f, -0.106f, -94);
                    yield return frame;
                    PlaceWeapon(-0.101f, -0.12f, 252);
                    yield return frame;
                    PlaceWeapon(-0.126f, -0.086f, 228);
                    yield return frame;
                    PlaceWeapon(-0.197f, -0.062f, 204);
                    yield return frame;
                    PlaceWeapon(-0.232f, -0.007f, 180);
                    yield return frame;
                    break;
                case "up":
                    PlaceWeapon(-0.113f, 0.264f, 90);
                    yield return frame;
                    PlaceWeapon(-0.091f, 0.383f, 70);
                    yield return frame;
                    PlaceWeapon(-0.002f, 0.455f, 50);
                    yield return frame;
                    PlaceWeapon(0.09f, 0.47f, 30);
                    yield return frame;
                    PlaceWeapon(0.208f, 0.426f, 10);
                    yield return frame;
                    PlaceWeapon(0.307f, 0.358f, -10);
                    yield return frame;
                    PlaceWeapon(0.365f, 0.258f, -30);
                    yield return frame;
                    PlaceWeapon(0.382f, 0.161f, -50);
                    yield return frame;
                    PlaceWeapon(0.346f, 0.04f, -70);
                    yield return frame;
                    PlaceWeapon(0.265f, -0.041f, -90);
                    yield return frame;
                    break;
                case "left":
                    PlaceWeapon(-0.314f, 0.258f, 45 + (45 - 90));
                    yield return frame;
                    PlaceWeapon(-0.349f, 0.229f, 45 + (45 - 70));
                    yield return frame;
                    PlaceWeapon(-0.357f, 0.216f, 45 + (45 - 50));
                    yield return frame;
                    PlaceWeapon(-0.357f, 0.216f, 45 + (45 - 30));
                    yield return frame;
                    PlaceWeapon(-0.336f, 0.171f, 45 + (45 - 10));
                    yield return frame;
                    PlaceWeapon(-0.299f, 0.131f, 45 + (45 - -10));
                    yield return frame;
                    PlaceWeapon(-0.264f, 0.076f, 45 + (45 - -30));
                    yield return frame;
                    PlaceWeapon(-0.244f, 0.07f, 45 + (45 - -50));
                    yield return frame;
                    PlaceWeapon(-0.209f, 0.048f, 45 + (45 - -70));
                    yield return frame;
                    PlaceWeapon(-0.165f, 0.018f, 45 + (45 - -90));
                    yield return frame;
                    break;
            }
            isAttacking = false;
        }

        if (Weapon._class == "Archer")
        {
            switch (dir)
            {
                case "right":
                    _sprite.sortingOrder = 100;
                    PlaceWeapon(0.369f, 0.146f, 0);
                    yield return new WaitForSeconds(0.16f);
                    break;
                case "up":
                    PlaceWeapon(0.008f, 0.396f, 90);
                    _sprite.sortingOrder = -10000;
                    yield return new WaitForSeconds(0.16f);
                    break;
                case "down":
                    _sprite.sortingOrder = 100;
                    PlaceWeapon(-0.06f, -0.18f, -90);
                    yield return new WaitForSeconds(0.16f);
                    break;
                case "left":
                    _sprite.sortingOrder = 100;
                    PlaceWeapon(-0.359f, 0.157f, 180);
                    yield return new WaitForSeconds(0.16f);
                    break;
            }
        }
        
        
        switch (dir)
        {
            case "down" :
                PlayerMoveEvent.Raise(new EventArgsCoor(0, -1, int.Parse((string) Coffre.Regarder("id"))));
                yield break;
            case "up" :
                PlayerMoveEvent.Raise(new EventArgsCoor(0, 1, int.Parse((string)Coffre.Regarder("id"))));
                yield break;
            case "left" :
                PlayerMoveEvent.Raise(new EventArgsCoor(-1, -1, int.Parse((string)Coffre.Regarder("id"))));
                yield break;
            case "right" :
                PlayerMoveEvent.Raise(new EventArgsCoor(1, 1, int.Parse((string)Coffre.Regarder("id"))));
                yield break;
        }
    }

    public void WeaponMovement(IEventArgs args)
    {
        if (Weapon.inPlayerInventory)
        {
            if (Weapon._class == "Warrior")
            {
                EventArgsCoor move = args as EventArgsCoor;
                if (move.X == 0 && move.Y == 0) return;
                bool dir = Math.Abs(move.X) < Math.Abs(move.Y);
                if (dir)
                {
                    if (move.Y > 0) //up
                    {
                        PlaceWeapon(-0.258f, 0.246f, -90);
                        _sprite.sortingOrder = 101;
                    }

                    else
                    {
                        PlaceWeapon(0.261f, 0.246f, 180);
                        _sprite.sortingOrder = -10000;
                    }
                }
                else
                {
                    if (move.X < 0)
                    {
                        PlaceWeapon(0.223f, 0.353f, 195);
                        _sprite.sortingOrder = -10000;
                    }
                    else
                    {
                        PlaceWeapon(0.062f, 0.431f, 205);
                        _sprite.sortingOrder = -10000;
                    }
                }
            }


            if (Weapon._class == "Archer")
            {
                EventArgsCoor move = args as EventArgsCoor;
                if (move.X == 0 && move.Y == 0) return;
                bool dir = Math.Abs(move.X) < Math.Abs(move.Y);
                if (dir)
                {
                    if (move.Y > 0) //up
                    {
                        PlaceWeapon(0.118f, 0.117f, 45);
                        _sprite.sortingOrder = 101;
                    }

                    else // down
                    {
                        PlaceWeapon(-0.11f, 0.178f, 135);
                        _sprite.sortingOrder = -10000;
                    }
                }
                else
                {
                    if (move.X < 0) //right
                    {
                        PlaceWeapon(-0.143f, 0.163f, 150);
                        _sprite.sortingOrder = -10000;
                    }
                    else //left
                    {
                        PlaceWeapon(-0.094f, 0.095f, 165);
                        _sprite.sortingOrder = -10000;
                    }
                }
            }

            if (Weapon._class == "Assassin")
            {
                EventArgsCoor move = args as EventArgsCoor;
                if (move.X == 0 && move.Y == 0) return;
                bool dir = Math.Abs(move.X) < Math.Abs(move.Y);
                if (dir && move.Y > 0)
                {
                    transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
                    PlaceWeapon(0f, -0.04f, 90);
                    _sprite.sortingOrder = 1000;
                }
                else
                {
                    transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
                }
            }
        }
    }

    public void PlaceWeapon(float x, float y, float angle)
    {
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.localPosition = new Vector3(x, y);
    }
    
    IEnumerator MageOrbMovement()
    {
        float y = 0.3f;
        float a = 0.7f;
        float b = 0.15f;
        float alpha = 0;

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        while (Weapon.inPlayerInventory)
        {
            alpha += 0.04f;
            float deltaY = (float)(b * Math.Sin(alpha));
            if(deltaY > 0) sprite.sortingOrder = (int) transform.position.y * -11;
            else sprite.sortingOrder = (int) transform.position.y * -9;
            transform.localScale = Vector3.one * (-deltaY / 1.5f + 1);
            transform.localPosition = new Vector3((float)(a * Math.Cos(alpha)), (y + deltaY), transform.position.z);
             
            yield return null;
        }
    }

    private void Update()
    {
        if (Weapon.inPlayerInventory)
        {
            if (Weapon._class == "Mage")
            {
                StartCoroutine(MageOrbMovement());
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mouse"))
        {
            AthItem.Raise(new EventArgsItemAth(Weapon));
        }
        
        if (_playerData.Name == "Warrior")
        {
            if (isAttacking && other.CompareTag("Enemies"))
            {
                EnemiesLive s = other.gameObject.GetComponent<EnemiesLive>();
                s.GetDamaged(_playerData.PhysicsDamage + Weapon.PhysicsDamage + comp.AdDamage, _playerData.MagicDamage + Weapon.MagicDamage + comp.ApDamage);
                if(Weapon.EffectType != 0) s.Effect(Weapon.EffectType, Weapon.EffectDamage, Weapon.Duration);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Mouse"))
        {
            AthItemS.Raise(new EventArgsItemAth(Weapon));
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if ((other.transform.position - transform.position).sqrMagnitude < 0.5)
            {
                if (!wait && Input.GetKey(KeyCode.A))
                {
                    StartCoroutine(Wait());
                    AddItem.Raise(new EventArgsItem(Weapon, transform));
                }
            }
        }
    }
    
    IEnumerator Wait()
    {
        wait = true;
        yield return new WaitForSeconds(0.5f);
        wait = false;
    }
    
    public void DestroyWeapon(IEventArgs args)
    {
        Destroy(gameObject);
    }
}
