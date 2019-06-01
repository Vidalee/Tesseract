using System.Collections;
using Script.GlobalsScript.Struct;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    #region Variable

    public PlayerData _playerData;
    [SerializeField] protected LayerMask BlockingLayer;
    [SerializeField] protected GameEvent PlayerDashEvent;

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
        if (!_playerData.CanMove) return;
        
        if (Input.GetKey("space") && _playerData.GetCompetence("Dash").Usable)
        {
            if (_playerData.Name == "Mage") StartCoroutine(Dash(_playerData.GetCompetence("Dash") as DashComp));
            else StartCoroutine(SmoothDash(_playerData.GetCompetence("Dash") as DashComp));
        }
    }


    #endregion

    #region Dash

    private IEnumerator Dash(DashComp competence)
    {
        competence.Usable = false;
        _playerData.CanMove = false;

        Vector3 direction = CheckObstacles(Direction(), competence);
        
        yield return new WaitForSeconds(0.5f);

        transform.position += direction - direction * 0.01f;
        _playerData.CanMove = true;

        yield return new WaitForSeconds(competence.Cooldown);
        competence.Usable = true;
    }
    
    private IEnumerator SmoothDash(DashComp competence)
    {
        competence.Usable = false;
        _playerData.CanMove = false;
        
        Vector3 direction = CheckObstacles(Direction(), competence);
        float step = competence.DistDash * Time.fixedDeltaTime;
        float t = 0;
        Vector3 end = transform.position + direction - direction * 0.01f;
        
        while ((end - transform.position).magnitude > 0.1f)
        {
            t += step;
            transform.position = Vector3.Lerp(transform.position, end, t);
            yield return new WaitForFixedUpdate();
        }

        transform.position = end;
        _playerData.CanMove = true;
        yield return new WaitForSeconds(competence.Cooldown);
        competence.Usable = true;
    }

    #endregion

    #region Utilities

    private Vector3 Direction()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0;
        return (cursorPos - transform.position).normalized;
    }

    private Vector3 CheckObstacles(Vector3 dir, DashComp competence)
    {
        int xDir = dir.x > 0 ? 1 : -1;
        int yDir = dir.y > 0 ? 1 : -1;
        
        PlayerDashEvent.Raise(new EventArgsNull());
        
        Vector3 playerPos = transform.position - new Vector3(0, _playerData.Height/2);
        if (yDir > 0) playerPos.y += _playerData.FeetHeight;
        
        Vector3 playerPosleft = playerPos + new Vector3(_playerData.Width / 2 * xDir, 0, 0);
        Vector3 playerPosRight = playerPos + new Vector3(-_playerData.Width / 2 * xDir, 0, 0);
        
        RaycastHit2D rayL = Physics2D.Raycast(playerPosleft, dir * competence.DistDash, competence.DistDash, BlockingLayer);
        RaycastHit2D rayR = Physics2D.Raycast(playerPosRight, dir * competence.DistDash, competence.DistDash, BlockingLayer);

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

        return dir * competence.DistDash;
    }

    #endregion
}
