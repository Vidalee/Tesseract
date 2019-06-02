using System.Collections;
using System.Collections.Generic;
using Script.GlobalsScript.Struct;
using UnityEngine;

public class BossLive : MonoBehaviour
{
    public GameEvent SendPlayerXp;
    public GameEvent BossDeath;

    [SerializeField] protected int _hp;
    [SerializeField] protected int _armorP;
    [SerializeField] protected int _armorM;
    [SerializeField] protected int _xp;
    
    private bool alive = true;
    

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
        }
    }
}
