﻿using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Script.GlobalsScript.Struct;
using UnityEngine;

public class BossLive : MonoBehaviour
{
    public GameEvent SendPlayerXp;
    public GameEvent BossDeath;

    [SerializeField] protected int _hp;
    [SerializeField] protected int _Maxhp;
    [SerializeField] protected int _armorP;
    [SerializeField] protected int _armorM;
    [SerializeField] protected int _xp;

    [SerializeField] protected RectTransform Image; 
    
    private bool alive = true;


    public void Update()
    {
        Image.localScale = new Vector3((float) _hp / _Maxhp * 0.5025f, Image.localScale.y); 
    }
    public void GetDamaged(int damageP, int damageM)
    {

        int dP = damageP - _armorP;
        int dM = damageM - _armorM;
        
        if(dP < 0) dP = 0;
        if (dM < 0) dM = 0;

        _hp -= dP + dM;
            
        if (_hp <= 0 && alive)
        {
            alive = false;
            SendPlayerXp.Raise(new EventArgsInt(_xp));
            BossDeath.Raise(null);
            Destroy(transform.gameObject);
        }
    }
}
