using System.Collections;
using Script.GlobalsScript.Struct;
using UnityEngine;

public class PlayerDash : MonoBehaviour, UDPEventListener
{
    #region Variable

    public PlayerData _playerData;
    [SerializeField] protected LayerMask BlockingLayer;
    [SerializeField] protected GameEvent PlayerDashEvent;

    #endregion

    #region Inter-Thread

    private bool shouldDash = false;
    float dx = 0;
    float dy = 0;

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
        if (!_playerData.CanMove) return;

        if (Input.GetKey("space") && _playerData.GetCompetence("Dash").Usable)
        {
            if (_playerData.MultiID + "" == (string)Coffre.Regarder("id"))
            {
                if (_playerData.Name == "Mage") StartCoroutine(Dash(_playerData.GetCompetence("Dash")));
                else StartCoroutine(SmoothDash(_playerData.GetCompetence("Dash")));
            }
        }

        if (shouldDash)
        {
            shouldDash = false;
            if (_playerData.MultiID == 3) StartCoroutine(Dash(_playerData.GetCompetence("Dash"), dx, dy));
            else StartCoroutine(SmoothDash(_playerData.GetCompetence("Dash"), dx, dy));
        }
    }


    public void OnReceive(string text)
    {
        string[] args = text.Split(' ');
        if (text.StartsWith("PINFO"))
        {
            if (args[1] == (_playerData.MultiID + "") && args[2] == "DASH")
            {
                Debug.Log(_playerData.MultiID + " dash!!!");
                shouldDash = true;
                dx = float.Parse(args[3]);
                dy = float.Parse(args[4]);
            }
        }
    }
    #endregion

    #region Dash

    private IEnumerator Dash(CompetencesData competence, float dx = 0, float dy = 0)
    {
        competence.Usable = false;
        _playerData.CanMove = false;
        Vector3 dir = dx == 0 && dy == 0 ? Direction() : new Vector3(dx, dy, 0);
        if ((string)Coffre.Regarder("mode") == "multi" && _playerData.MultiID + "" == (string)Coffre.Regarder("id"))
            MultiManager.socket.Send("PINFO DASH " + dir.x + " " + dir.y);
        Debug.Log(_playerData.MultiID + " dashing to " + dir.x + " " + dir.y + " ( " + dx + " " + dy);
        Vector3 direction = CheckObstacles(dir, competence);
        yield return new WaitForSeconds(0.5f);

        transform.position += direction - direction * 0.01f;
        _playerData.CanMove = true;

        yield return new WaitForSeconds(competence.Cooldown);
        competence.Usable = true;
    }

    private IEnumerator SmoothDash(CompetencesData competence, float dx = 0, float dy = 0)
    {
        competence.Usable = false;
        _playerData.CanMove = false;
        Vector3 dir = dx == 0 && dy == 0 ? Direction() : new Vector3(dx, dy, 0);
        if ((string)Coffre.Regarder("mode") == "multi" && _playerData.MultiID + "" == (string)Coffre.Regarder("id"))
            MultiManager.socket.Send("PINFO DASH " + dir.y + " " + dir.x);
        Debug.Log(_playerData.MultiID + " dashing to " + dir.x + " " + dir.y + " ( " + dx + " " + dy);
        Vector3 direction = CheckObstacles(dir, competence);
       
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
        Debug.Log((cursorPos - transform.position).normalized.ToString());
        return (cursorPos - transform.position).normalized;
    }

    private Vector3 CheckObstacles(Vector3 dir, CompetencesData competence)
    {
        int xDir = dir.x > 0 ? 1 : -1;
        int yDir = dir.y > 0 ? 1 : -1;

        PlayerDashEvent.Raise(new EventArgsInt(_playerData.MultiID));

        Vector3 playerPos = transform.position - new Vector3(0, _playerData.Height / 2);
        if (yDir > 0) playerPos.y += _playerData.FeetHeight;

        Vector3 playerPosleft = playerPos + new Vector3(_playerData.Width / 2 * xDir, 0, 0);
        Vector3 playerPosRight = playerPos + new Vector3(-_playerData.Width / 2 * xDir, 0, 0);

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


    #endregion
}
