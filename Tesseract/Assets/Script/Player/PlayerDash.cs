using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    
    [SerializeField] protected PlayerData PlayerData;
    [SerializeField] protected LayerMask BlockingLayer;
    [SerializeField] protected GameEvent PlayerDashEvent;

    private void FixedUpdate()
    {
        if (Input.GetKey("space") && PlayerData.GetCompetence("Dash").Usable)
        {
            StartCoroutine(Dash(PlayerData.GetCompetence("Dash")));
        }
    }

    private Vector3 Direction()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0;
        return (cursorPos - transform.position).normalized;
    }

    private RaycastHit2D CheckObstacles(Vector3 dir, CompetencesData competence)
    {
        int xDir = dir.x > 0 ? 1 : -1;
        int yDir = dir.y > 0 ? 1 : -1;
        
        PlayerDashEvent.Raise(new EventArgsNull());
        
        Vector3 playerPos = transform.position + new Vector3(PlayerData.Width / 2 * xDir, PlayerData.Height/2 * yDir, 0);

        return Physics2D.Raycast(playerPos, dir, competence.Speed, BlockingLayer);
    }
    
    IEnumerator Dash(CompetencesData competence)
    {
        competence.Usable = false;
        Vector3 dir = Direction();

        //Dash to the wall if there is one, or dash to the normal position
        RaycastHit2D hit = CheckObstacles(dir, competence);
        if (!hit)
            transform.position += dir * competence.Speed;
        else
            transform.position += dir * hit.distance;
        
        yield return new WaitForSeconds(competence.Cooldown);
        competence.Usable = true;
    }
}
