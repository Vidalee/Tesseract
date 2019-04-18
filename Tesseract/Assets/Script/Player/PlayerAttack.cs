 using System;
 using System.Collections;
 using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] protected PlayerData PlayerData;
    [SerializeField] protected GameEvent AttackEvent;

    private void FixedUpdate()
    {
         if (Input.GetMouseButton(0))
         {
             UseCompetence(PlayerData.GetCompetence("AutoAttack"));
         }

        if (Input.GetKey("e"))
        {
            UseCompetence(PlayerData.GetCompetence("TripleShuriken"));
        }
     }

    private void UseCompetence(CompetencesData competence)
    {
        if (competence.Usable)
        {
            switch (competence.Name)
            {
                case "AutoAttack":
                    AutoAttack(competence);
                    break;
                case "TripleShuriken":
                    TripleShuriken(competence);
                    break;
            }
        }
    }

    private void AutoAttack(CompetencesData competence)
    {
        AttackEvent.Raise(new EventArgsNull());
        InstantiateProjectiles(competence, ProjectilesDirection());
        StartCoroutine(CoolDownCoroutine(competence, true));
    }
    
    private void TripleShuriken(CompetencesData competence)
    {
        AttackEvent.Raise(new EventArgsNull());
        Vector3 dir = ProjectilesDirection();
        Vector3 dir1 = Quaternion.Euler(0, 0, -10) * dir;
        Vector3 dir2 = Quaternion.Euler(0, 0, 10) * dir;

        InstantiateProjectiles(competence, dir);
        InstantiateProjectiles(competence, dir1);
        InstantiateProjectiles(competence, dir2);

        StartCoroutine(CoolDownCoroutine(competence, true));
    }

    private Vector3 ProjectilesDirection()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0;
        return (cursorPos - transform.position).normalized;
    }

    IEnumerator CoolDownCoroutine(CompetencesData competence, bool use)
    {
        competence.Usable = !use;
        yield return new WaitForSeconds(competence.Cooldown);
        competence.Usable = true;

    }
    
    private void InstantiateProjectiles(CompetencesData competence, Vector3 dir)
    {
        Transform o = Instantiate(competence.Object, transform.position + dir / 2, Quaternion.identity);
        o.name = competence.Name;
                
        ProjectilesData projectilesData = ScriptableObject.CreateInstance<ProjectilesData>();
        projectilesData.Created(dir, competence.Speed, competence.Damage, competence.Tag, competence.AnimationClip);
        
        Projectiles script = o.GetComponent<Projectiles>();

        script.Create(projectilesData);
    }
}
