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
                
        ProjectilesData projectilesData = ScriptableObject.CreateInstance<ProjectilesData>();
        projectilesData.Created(dir, competence.Speed, competence.Damage, competence.Tag, competence.AnimationClip);

        
        Projectiles script = o.GetComponent<Projectiles>();

        script.Create(projectilesData);
        
        yield return new WaitForSeconds(competence.Cooldown);
        competence.Usable = true;
    }
}
