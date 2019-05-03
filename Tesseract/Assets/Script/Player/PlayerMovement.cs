using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region MyRegion

    public PlayerData _playerData;
    public LayerMask BlockingLayer;
    public GameEvent PlayerMoveEvent;
    public GameEvent PlayerPos;
    

    #endregion

    #region Initialise

    public void Create(PlayerData playerData)
    {
        _playerData = playerData;

        StartCoroutine(UpdatePlayerPos());
    }


    #endregion

    #region Update

    private void Update()
    {
        PlayerMove();
    }

    #endregion

    #region Movement

        private void PlayerMove()
    {
        if (!_playerData.CanMove)
        {
            return;
        }
        
        int xDir = (int) Input.GetAxisRaw("Horizontal");
        int yDir = (int) Input.GetAxisRaw("Vertical");

        PlayerMoveEvent.Raise(new EventArgsCoor(xDir, yDir));

        if (xDir == 0 && yDir == 0) return;

        Vector3 distance = GetDistance(xDir, yDir);

        transform.Translate(distance * _playerData.MoveSpeed *Time.deltaTime);
    }

    private IEnumerator UpdatePlayerPos()
    {
        for(;;)
        {
            Vector3 position = transform.position;
            PlayerPos.Raise(new EventArgsCoor((int) position.x, (int) position.y));
            yield return new WaitForSeconds(0.1f);
        }
    }

    #endregion

    #region Utilities

    private Vector3 GetDistance(int xDir, int yDir)
    {
        Vector3 playerPos = transform.position;
        playerPos.y += -_playerData.Height / 2;
        Vector3 playerWidth = new Vector3(_playerData.Width / 2, 0, 0);
        Vector3 direction = new Vector3(xDir,yDir,0);
        
        RaycastHit2D xLinecastUp = Physics2D.Linecast(playerPos + new Vector3(0, _playerData.FeetHeight) + xDir * playerWidth,
            playerPos + xDir * playerWidth + new Vector3(xDir, _playerData.FeetHeight), BlockingLayer);
        RaycastHit2D xLinecastDown = Physics2D.Linecast(playerPos + xDir * playerWidth, playerPos + xDir * playerWidth + new Vector3(xDir, 0), BlockingLayer);
        
        /* DEBUG
        Vector3 s = playerPos + new Vector3(0, _playerData.FeetHeight) + xDir * playerWidth;
        Vector3 en = playerPos + xDir * playerWidth + new Vector3(xDir, _playerData.FeetHeight);
        Vector3 dir3 = (en - s);
        Debug.DrawRay(s, dir3, Color.red, 1000f, false);
        */
        
        if (yDir > 0)
        {
            playerPos.y += _playerData.FeetHeight;
        }

        RaycastHit2D yLeftLinecast = Physics2D.Linecast(playerPos + playerWidth, playerPos + playerWidth + new Vector3(0, yDir), BlockingLayer);
        RaycastHit2D yRightLinecast = Physics2D.Linecast(playerPos - playerWidth, playerPos - playerWidth + new Vector3(0, yDir), BlockingLayer);

        if (xLinecastDown && xLinecastUp)
        {
            direction.x *= xLinecastDown.distance < xLinecastUp.distance
                ? xLinecastDown.distance
                : xLinecastUp.distance;
        }
        else if (xLinecastDown)
        {
             direction.x *= xLinecastDown.distance - 0.01f;
        }
        else if (xLinecastUp)
        {
            direction.x *= xLinecastUp.distance - 0.01f;
        }

        if (yLeftLinecast && yRightLinecast)
        {
            direction.y *= yLeftLinecast.distance < yRightLinecast.distance
                ? yLeftLinecast.distance
                : yRightLinecast.distance;
        }
        if (yLeftLinecast)
        {
            direction.y *= yLeftLinecast.distance - 0.01f;
        }
        else if (yRightLinecast)
        {
            direction.y *= yRightLinecast.distance - 0.01f;
        }

        if (!xLinecastDown && !xLinecastUp && !yRightLinecast && !yLeftLinecast)
        {
            RaycastHit2D diagLinecastLeft = Physics2D.Linecast(playerPos + playerWidth, playerPos + playerWidth + direction, BlockingLayer);
            RaycastHit2D diagLinecastRight = Physics2D.Linecast(playerPos - playerWidth, playerPos - playerWidth + direction, BlockingLayer);

            if (diagLinecastLeft && diagLinecastRight)
            {
                if (diagLinecastLeft.distance < diagLinecastRight.distance)
                {
                    direction *= diagLinecastLeft.distance - 0.01f;
                }
                else
                {
                    direction *= diagLinecastRight.distance - 0.01f;
                }
            }
            else
            {
                if (diagLinecastLeft) direction *= diagLinecastLeft.distance - 0.01f;
                else if(diagLinecastRight) direction *= diagLinecastRight.distance - 0.01f;
            }
        }

        return direction;
    }

    #endregion
}