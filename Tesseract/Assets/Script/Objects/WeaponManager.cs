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
    public bool isAttacking = false;

    public AnimationClip attack;
    public int compteur = 0;

    public GameEvent AthItem;
    public GameEvent AthItemS;
    public GameEvent AddItem;
    public bool wait;
    
    public void Create(Weapons weapon)
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = weapon.icon;
        if (!weapon.inPlayerInventory) _sprite.sortingOrder = (int) transform.position.y * -15;
        Weapon = weapon;
        EdgeCollider2D collider = GetComponent<EdgeCollider2D>();
        collider.points = weapon.ColliderPoints;  
    }
    
    public void AttackWithWeapon(IEventArgs args)
    {
        if (Weapon.inPlayerInventory)
        {
            if (Weapon._class == "Warrior")
            {
                Vector3 move = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                bool dir = Math.Abs(move.x) < Math.Abs(move.y);
                if (dir)
                {
                    if (move.y < 0)
                    {
                        StartCoroutine(Attack("up"));
                    }
                    else
                    {
                        StartCoroutine(Attack("down"));
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
                        // Up -> x : 0.208 / y : 0.362
                        // Down -> x : 0.246 / y : -0.065

                        // Center -> x : 0.112 / y : 0.148

                    }
                }
            }
        }
    }
    
    IEnumerator Attack(string dir)
    {
        isAttacking = true;
        
        WaitForSeconds frame = new WaitForSeconds(0.008f);
        
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

        isAttacking = false;
        
        switch (dir)
        {
            case "up" :
                PlayerMoveEvent.Raise(new EventArgsCoor(0, -1));
                yield break;
            case "down" :
                PlayerMoveEvent.Raise(new EventArgsCoor(0, 1));
                yield break;
            case "left" :
                PlayerMoveEvent.Raise(new EventArgsCoor(-1, -1));
                yield break;
            case "right" :
                PlayerMoveEvent.Raise(new EventArgsCoor(1, 1));
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
                    if (move.Y > 0)
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
            transform.localScale = Vector3.one * (-deltaY / 5f + 1);
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

        if (isAttacking && other.CompareTag("Enemies"))
        {
            EnemiesLive s = other.gameObject.GetComponent<EnemiesLive>();
            s.GetDamaged(Weapon.PhysicsDamage + Weapon.MagicDamage);
            if(Weapon.EffectType != 0) s.Effect(Weapon.EffectType, Weapon.EffectDamage, Weapon.Duration);
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
