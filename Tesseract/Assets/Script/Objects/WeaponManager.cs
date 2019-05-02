using System;
using System.Collections;
using System.Collections.Generic;
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

    
    public void PlaceWeapon(IEventArgs args)
    {
        /*
        Vector3 move = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        bool dir =  Math.Abs(move.x) < Math.Abs(move.y);
        if (dir)
        {
            if (move.y < 0)
            {
                StartCoroutine(Attack(-0.052f, -0.002f, -0.122f, -0.009f));
                Debug.Log("Down");
            }
            else
            {
                StartCoroutine(Attack(0.049f, -0.007f, -0.218f, -0.014f));
                Debug.Log("Up");
            }
        }
        else
        {
            if (move.x < 0)
            {
                StartCoroutine(Attack(0.049f, -0.007f, -0.218f, -0.014f));
                Debug.Log("Left");
            }
            else 
            {
                StartCoroutine(Attack(0.049f, -0.007f, -0.218f, -0.014f));
                Debug.Log("Right"); 
            }  
        }
        */
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
