 using System;
 using System.Collections;
 using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] protected PlayerData PlayerData;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
         if (Input.GetMouseButton(0) && PlayerData.GetCompetence("AutoAttack").Usable)
         {
             StartCoroutine(InstantiateProjectiles(PlayerData.GetCompetence("AutoAttack"), ProjectilesDirection()));
             StartCoroutine(Shuriken1A());
         }
     }

    private Vector3 ProjectilesDirection()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0;
        return (cursorPos - transform.position).normalized;
    }

    IEnumerator InstantiateProjectiles(CompetencesData competence, Vector3 dir)
    {
        competence.Usable = false;
        Transform o = Instantiate(competence.Object, transform.position + dir / 2, Quaternion.identity, transform);
        o.name = competence.Name;
        
        Projectiles script = o.GetComponent<Projectiles>();
        script.Create(dir, competence.Speed, competence.Damage, "Enemy", competence.AnimationClip);
        
        yield return new WaitForSeconds(competence.Cooldown);
        competence.Usable = true;
    }
    
    IEnumerator Shuriken1A()
    {       
        _animator.SetBool("OtherAction", true);
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        bool dir = Math.Abs(diff.x) > Math.Abs(diff.y);

        if (dir)
        {
            _animator.Play(diff.x < 0 ? "DefaultThrowL" : "DefaultThrowT");
        }
        else
        {
            _animator.Play(diff.y < 0 ? "DefaultThrowB" : "DefaultThrowT");
        }

        yield return new WaitForSeconds(0.2f);
        _animator.SetBool("OtherAction", false);
    }
}
