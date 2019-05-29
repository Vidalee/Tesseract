using System;
using System.Collections;
using System.Collections.Generic;
using Script.GlobalsScript;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] protected Weapons Weapon;
    [SerializeField] private SpriteRenderer _sprite;
    
    
    public void Create(Weapons weapon)
    {
        _sprite = GetComponent<SpriteRenderer>();
        Weapon = weapon;
        EdgeCollider2D collider = GetComponent<EdgeCollider2D>();
        collider.points = weapon.ColliderPoints;  
        
    }

    
    public void AttackWithWeapon(IEventArgs args)
    {
        
        Vector3 move = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        bool dir =  Math.Abs(move.x) < Math.Abs(move.y);
        if (dir)
        {
            if (move.y < 0)
            {
                StartCoroutine(Attack(-0.052f, -0.002f, -0.122f, -0.009f));
            }
            else
            {
                StartCoroutine(Attack(0.049f, -0.007f, -0.218f, -0.014f));
            }
        }
        else
        {
            if (move.x < 0)
            {
                StartCoroutine(Attack(0.049f, -0.007f, -0.218f, -0.014f));
                
            }
            else 
            {
                StartCoroutine(Attack(0.049f, -0.007f, -0.218f, -0.014f));
                // Up -> x : 0.208 / y : 0.362
                // Down -> x : 0.246 / y : -0.065
                
                // Center -> x : 0.112 / y : 0.148
                
            }  
        }
    }


    public void WeaponMovement(IEventArgs args)
    {
        EventArgsCoor move = args as EventArgsCoor;
        if (move.X == 0 && move.Y == 0) return;
        bool dir =  Math.Abs(move.X) < Math.Abs(move.Y);
        if (dir && move.Y > 0)
        {
            _sprite.enabled = true;
        }
        else _sprite.enabled = false;
    }


    public void PlaceWeapon(float x, float y, float angle)
    {
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.localPosition = new Vector3(x, y);
    }
    
    IEnumerator Attack(float a, float b, float c, float d)
    {
        _sprite.enabled = true;
        var position = transform.position;
        position = new Vector3(a, b);
        yield return new WaitForSeconds(0.25f);
        transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
        position = new Vector3(c, d);
        yield return new WaitForSeconds(0.25f);
        transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        _sprite.enabled = false;
    }
    
    
    
}
