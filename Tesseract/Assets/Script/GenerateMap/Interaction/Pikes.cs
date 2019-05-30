using System.Collections;
using System.Collections.Generic;
using Script.GlobalsScript.Struct;
using UnityEngine;

public class Pikes : MonoBehaviour
{
    private PikesData _pikesData;
    public GameEvent PlayerDamage;
    private SpriteRenderer _spriteRenderer;
    public void Create(PikesData pikesData)
    {
        _pikesData = pikesData;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _pikesData.NonTrig;
        _spriteRenderer.sortingOrder = (int) (transform.position.y * -15);

    }

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerFeet"))
        {
            yield return new WaitForSeconds(1);
            _spriteRenderer.sprite = _pikesData.Trig;

            if ((other.transform.position - transform.position - new Vector3(0, 0.5f, 0)).magnitude < 0.5f)
            {
                PlayerDamage.Raise(new EventArgsInt(_pikesData.Damage));
            }
            
            yield return new WaitForSeconds(1);
            _spriteRenderer.sprite = _pikesData.NonTrig;
        }
    }
}
