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
            UseCompetence(PlayerData.Competences[1]);
        }

        if (Input.GetKey("r"))
        {
            UseCompetence(PlayerData.Competences[2]);
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
                    TripleAttack(competence);
                    break;
                case "EnergyBall":
                    CircleAttack(competence);
                    break;
            }
        }
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
        projectilesData.Created(dir, competence.Speed, competence.Damage, competence.Tag, competence.AnimationClip, competence.Live, competence.Color);
        
        Projectiles script = o.GetComponent<Projectiles>();

        script.Create(projectilesData);
    }
    
    private void AutoAttack(CompetencesData competence)
    {
        AttackEvent.Raise(new EventArgsNull());
        InstantiateProjectiles(competence, ProjectilesDirection());
        StartCoroutine(CoolDownCoroutine(competence, true));
    }
    
    //Ninja triple shuriken
    private void TripleAttack(CompetencesData competence)
    {
        AttackEvent.Raise(new EventArgsNull());
        float rotDist = 10;
        float rot = rotDist;
        Vector3 dir = ProjectilesDirection();
        InstantiateProjectiles(competence, dir);
        
        for (int i = 0; i < competence.Number/2; i++)
        {
            InstantiateProjectiles(competence, Quaternion.Euler(0, 0, rot) * dir);
            InstantiateProjectiles(competence, Quaternion.Euler(0, 0, -rot) * dir);
            
            rot += rotDist;
        }
        
        StartCoroutine(CoolDownCoroutine(competence, true));
    }
    
    //Mage multiple eneryball
    private void CircleAttack(CompetencesData competence)
    {
        AttackEvent.Raise(new EventArgsNull());
        float rot = 360 / competence.Number;
        
        for (int i = 0; i < competence.Number; i++)
        {
            InstantiateProjectiles(competence, Quaternion.Euler(0, 0, rot * i) * new Vector3(1, 1, 0));
        }

        StartCoroutine(CoolDownCoroutine(competence, true));
    }
}
