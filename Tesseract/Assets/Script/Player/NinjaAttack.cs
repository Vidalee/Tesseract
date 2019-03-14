using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaAttack : MonoBehaviour
{
    public int shuriken1MaxCooldown;
    public int shuriken1Speed;
    public Transform shuriken1Obj;
    public int shuriken1Damage;

    private int shuriken1Cooldown;

    private void Start()
    {
        shuriken1Cooldown = shuriken1MaxCooldown;
    }

    void FixedUpdate()
    {
        if (shuriken1MaxCooldown > shuriken1Cooldown)
        {
            shuriken1Cooldown++;
        }

        if (Input.GetMouseButtonDown(0))
        {
            ShurikenAttack();
        }
    }

    public void ShurikenAttack()
    {
        if (shuriken1Cooldown < shuriken1MaxCooldown)
        {
            return;
        }

        shuriken1Cooldown = 0;
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0;
        Vector3 shuriken1Direction = (cursorPos - transform.position).normalized;

        Transform shuriken1 = Instantiate(shuriken1Obj, transform.position + shuriken1Direction, Quaternion.identity);
        Projectiles scriptShuriken1 = shuriken1.GetComponent<Projectiles>();

        scriptShuriken1.Create(shuriken1Direction, shuriken1Speed, shuriken1Damage);
    }
}