using System.Collections;
using System.Collections.Generic;
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
    }

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            yield return new WaitForSeconds(1);
            _spriteRenderer.sprite = _pikesData.Trig;

            if ((other.transform.position - transform.position).magnitude < 0.5f)
            {
                PlayerDamage.Raise(new EventArgsInt(_pikesData.Damage));
            }
            
            yield return new WaitForSeconds(1);
            _spriteRenderer.sprite = _pikesData.NonTrig;
        }
    }
}
