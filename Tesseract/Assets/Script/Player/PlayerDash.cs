using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    
    [SerializeField] protected PlayerData PlayerData;
    [SerializeField] protected LayerMask BlockingLayer;
    [SerializeField] protected GameEvent PlayerDashEvent;

    private void FixedUpdate()
    {
        if (!PlayerData.CanMove) return;
        
        if (Input.GetKey("space") && PlayerData.GetCompetence("Dash").Usable)
        {
            if (PlayerData.Name == "Mage") StartCoroutine(Dash(PlayerData.GetCompetence("Dash")));
            else StartCoroutine(SmoothDash(PlayerData.GetCompetence("Dash")));
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


    private IEnumerator Dash(CompetencesData competence)
    {
        competence.Usable = false;
        PlayerData.CanMove = false;

        Vector3 direction = CheckObstacles(Direction(), competence);
        
        yield return new WaitForSeconds(0.5f);

        transform.position += direction;
        competence.Usable = true;
        PlayerData.CanMove = true;
    }
    
    private IEnumerator SmoothDash(CompetencesData competence)
    {
        competence.Usable = false;
        PlayerData.CanMove = false;
        
        Vector3 direction = CheckObstacles(Direction(), competence);
        float step = competence.Speed * Time.fixedDeltaTime;
        float t = 0;
        Vector3 end = transform.position + direction - direction * 0.01f;
        
        while ((end - transform.position).magnitude > 0.1f)
        {
            t += step;
            transform.position = Vector3.Lerp(transform.position, end, t);
            yield return new WaitForFixedUpdate();
        }

        transform.position = end;
        PlayerData.CanMove = true;
        yield return new WaitForSeconds(competence.Cooldown);
        competence.Usable = true;
    }
}
