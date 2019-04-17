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

    private Vector3 CheckObstacles(Vector3 dir, CompetencesData competence)
    {
        int xDir = dir.x > 0 ? 1 : -1;
        int yDir = dir.y > 0 ? 1 : -1;
        
        PlayerDashEvent.Raise(new EventArgsNull());
        
        Vector3 playerPos = transform.position - new Vector3(0, PlayerData.Height/2);
        if (yDir > 0) playerPos.y += PlayerData.FeetHeight;
        
        Vector3 playerPosleft = playerPos + new Vector3(PlayerData.Width / 2 * xDir, 0, 0);
        Vector3 playerPosRight = playerPos + new Vector3(-PlayerData.Width / 2 * xDir, 0, 0);
        
        RaycastHit2D rayL = Physics2D.Raycast(playerPosleft, dir * competence.Speed, competence.Speed, BlockingLayer);
        RaycastHit2D rayR = Physics2D.Raycast(playerPosRight, dir * competence.Speed, competence.Speed, BlockingLayer);

        if (rayL && rayR)
        {
            if (rayL.distance < rayR.distance)
            {
                return dir * rayL.distance;
            }

            return dir * rayR.distance;
        }

        if (rayL) return dir * rayL.distance;
        if (rayR) return dir * rayR.distance;

        return dir * competence.Speed;
    }
    
    IEnumerator Dash(CompetencesData competence)
    {
        competence.Usable = false;
        Vector3 dir = Direction();

        //Dash to the wall if there is one, or dash to the normal position
        transform.position += CheckObstacles(dir, competence) - dir * 0.01f;
        
        yield return new WaitForSeconds(competence.Cooldown);
        competence.Usable = true;
    }
}
