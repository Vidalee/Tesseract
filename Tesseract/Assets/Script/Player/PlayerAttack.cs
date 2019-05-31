 using System;
 using System.Collections;
 using Script.GlobalsScript.Struct;
 using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Variable

    public PlayerData _playerData;
    [SerializeField] protected GameEvent AttackEvent;

    #endregion

    #region Initialise

    public void Create(PlayerData playerData)
    {
        _playerData = playerData;
    }

    #endregion

    #region Update

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            UseCompetence(_playerData.GetCompetence("AutoAttack"));
        }

        if (Input.GetKey("e"))
        {
            UseCompetence(_playerData.Competences[1]);
        }

        if (Input.GetKey("r"))
        {
            UseCompetence(_playerData.Competences[2]);
        }
    }

    #endregion

    #region Competence

    private void UseCompetence(CompetencesData competence)
    {
        if (competence.Usable)
        {
            switch (competence.Name)
            {
                case "AutoAttack":
                    AutoAttack(competence);
                    break;
                case "MultipleProjectiles":
                    MultipleAttack(competence);
                    break;
                case "CirclesProjectiles":
                    CircleAttack(competence);
                    break;
            }
        }
    }

    private void AutoAttack(CompetencesData competence)
    {
        AttackEvent.Raise(new EventArgsNull());
        StartCoroutine(CoolDownCoroutine(competence, true));
        if (_playerData.Name == "Warrior") return;
        InstantiateProjectiles(competence, ProjectilesDirection());
 
    }
    
    private void MultipleAttack(CompetencesData competence)
    {
        AttackEvent.Raise(new EventArgsNull());
        if (_playerData.Name == "Warrior") return;
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
    
    private void CircleAttack(CompetencesData competence)
    {
        if (_playerData.Mana < competence.ManaCost)
        {
            return;
        }

        _playerData.Mana -= competence.ManaCost;

        AttackEvent.Raise(new EventArgsNull());
        if (_playerData.Name == "Warrior") return;
        float rot = 360 / competence.Number;
        
        for (int i = 0; i < competence.Number; i++)
        {
            InstantiateProjectiles(competence, Quaternion.Euler(0, 0, rot * i) * new Vector3(1, 1, 0));
        }

        StartCoroutine(CoolDownCoroutine(competence, true));
    }

    #endregion

    #region Utilities

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
        Transform o = Instantiate(competence.Object, transform.position + dir/4, Quaternion.identity);
        o.name = competence.Name;
                
        ProjectilesData projectilesData = ScriptableObject.CreateInstance<ProjectilesData>();
        projectilesData.Created(dir, competence.Speed, competence.Damage, competence.Tag, _playerData.AnimProj(), competence.Live, competence.Color);
        
        Projectiles script = o.GetComponent<Projectiles>();

        script.Create(projectilesData);
    }

    #endregion
}
