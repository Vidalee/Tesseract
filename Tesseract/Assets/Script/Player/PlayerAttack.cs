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
                    AutoAttack(competence as ProjComp);
                    break;
                case "MultipleProjectiles":
                    MultipleAttack(competence as ProjComp);
                    break;
                case "CirclesProjectiles":
                    CircleAttack(competence as ProjComp);
                    break;
            }
        }
    }

    private void AutoAttack(ProjComp competence)
    {
        AttackEvent.Raise(new EventArgsNull());
        StartCoroutine(CoolDownCoroutine(competence, true));
        if (_playerData.Name == "Warrior") return;
        InstantiateProjectiles(competence, ProjectilesDirection());
 
    }
    
    private void MultipleAttack(ProjComp competence)
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
    
    private void CircleAttack(ProjComp competence)
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
    
    private void InstantiateProjectiles(ProjComp competence, Vector3 dir)
    {
        Transform o = Instantiate(competence.Object, transform.position + dir/4, Quaternion.identity);
        o.name = competence.Name;
                
        ProjectilesData projectilesData = ScriptableObject.CreateInstance<ProjectilesData>();

        int dP = _playerData.PhysicsDamage + _playerData.Inventory.Weapon.PhysicsDamage + competence.AdDamage;
        int dM = _playerData.MagicDamage + _playerData.Inventory.Weapon.MagicDamage + competence.ApDamage;
        
        projectilesData.Created(dir, competence.Speed, dP, dM, competence.EnemyTag, _playerData.AnimProj(),
            competence.Live, _playerData.AnimColor(), _playerData.Prob(), _playerData.Effect(), _playerData.EffectDamage(), _playerData.Duration());
        
        Projectiles script = o.GetComponent<Projectiles>();

        script.Create(projectilesData);
    }

    #endregion
}
