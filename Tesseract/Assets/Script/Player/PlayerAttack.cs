 using System;
 using System.Collections;
 using Script.GlobalsScript.Struct;
 using UnityEngine;

public class PlayerAttack : MonoBehaviour, UDPEventListener
{
    #region Variable

    public PlayerData _playerData;
    [SerializeField] protected GameEvent AttackEvent;

    #endregion

    #region Inter-Thread

    private bool aa = false;
    private bool a1 = false;
    private bool a2 = false;
    private float dx = 0;
    private float dy = 0;
    private string action = "DEF";

    #endregion


    #region Initialise

    public void Create(PlayerData playerData)
    {
        _playerData = playerData;
        UDPEvent.Register(this);
    }

    #endregion

    #region Update

    private void FixedUpdate()
    {
        if (_playerData.MultiID + "" == (string)Coffre.Regarder("id"))
        {
            if (Input.GetMouseButton(0))
            {
                action = "AA";
                UseCompetence(_playerData.GetCompetence("AutoAttack"));
            }

            if (Input.GetKey("e"))
            {
                action = "A1";
                UseCompetence(_playerData.Competences[1]);
            }

            if (Input.GetKey("r"))
            {
                action = "A2";
                UseCompetence(_playerData.Competences[2]);
            }
        }

        if (aa)
        {
            aa = false;
            UseCompetence(_playerData.GetCompetence("AutoAttack"), dx, dy);
        }
        if (a1)
        {
            a1 = false;
            UseCompetence(_playerData.Competences[1], dx, dy);
        }
        if (a2)
        {
            a2 = false;
            UseCompetence(_playerData.Competences[2], dx, dy);
        }
    }

    public void OnReceive(string text)
    {
        string[] args = text.Split(' ');
        if (args[0] == "PINFO")
        {
            if (args[1] == (_playerData.MultiID + ""))
            {
                dx = float.Parse(args[3]);
                dy = float.Parse(args[4]);
                Debug.Log("parsed: dx " + dx + " dy " + dy);
                if (args[2] == "AA")
                {
                    aa = true;
                }
                else if (args[2] == "A1")
                {
                    a1 = true;
                }
                else if(args[2] == "A2")
                {
                    a2 = true;
                }
            }
        }
    }

    #endregion

    #region Competence

    private void UseCompetence(CompetencesData competence, float dx = 0, float dy = 0)
    {
        if (competence.Usable)
        {
            switch (competence.Name)
            {
                case "AutoAttack":
                    AutoAttack(competence, dx, dy);
                    break;
                case "MultipleProjectiles":
                    MultipleAttack(competence, dx, dy);
                    break;
                case "CirclesProjectiles":
                    CircleAttack(competence, dx, dy);
                    break;
            }
        }
    }

    private void AutoAttack(CompetencesData competence, float dx = 0, float dy = 0)
    {
        AttackEvent.Raise(new EventArgsInt(_playerData.MultiID));
        StartCoroutine(CoolDownCoroutine(competence, true));
        if (_playerData.Name == "Warrior") return;
        InstantiateProjectiles(competence, ProjectilesDirection(dx, dy));
    }
    
    private void MultipleAttack(CompetencesData competence, float dx = 0, float dy = 0)
    {
        AttackEvent.Raise(new EventArgsInt(_playerData.MultiID));
        if (_playerData.Name == "Warrior") return;
        float rotDist = 10;
        float rot = rotDist;
        Vector3 dir = ProjectilesDirection(dx, dy);
        InstantiateProjectiles(competence, dir);
        
        for (int i = 0; i < competence.Number/2; i++)
        {
            InstantiateProjectiles(competence, Quaternion.Euler(0, 0, rot) * dir);
            InstantiateProjectiles(competence, Quaternion.Euler(0, 0, -rot) * dir);
            
            rot += rotDist;
        }
        
        StartCoroutine(CoolDownCoroutine(competence, true));
    }
    
    private void CircleAttack(CompetencesData competence, float dx = 0, float dy = 0)
    {
        if (_playerData.Mana < competence.ManaCost)
        {
            return;
        }
        if(dx == 0 && dy == 0)
            MultiManager.socket.Send("PINFO " + action + " -1 " + dx);

        _playerData.Mana -= competence.ManaCost;

        AttackEvent.Raise(new EventArgsInt(_playerData.MultiID));
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

    private Vector3 ProjectilesDirection(float dx = 0, float dy = 0)
    {
        bool by = dy == 0;
        if (dx != 0 || dy != 0)
            return new Vector3(dx, dy, 0);
        else
        {
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorPos.z = 0;
            Vector3 n = (cursorPos - transform.position).normalized;
            MultiManager.socket.Send("PINFO " + action + " " + n.x + " " + n.y);
            return n;
        }
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
        projectilesData.Created(dir, competence.Speed, competence.Damage, competence.Tag, competence.AnimationClip, competence.Live, competence.Color);
        
        Projectiles script = o.GetComponent<Projectiles>();

        script.Create(projectilesData);
    }


    #endregion
}
