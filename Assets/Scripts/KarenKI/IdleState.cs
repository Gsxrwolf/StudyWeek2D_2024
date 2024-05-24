using UnityEngine;
using UnityEngine.UIElements;

public class IdleState : State
{
    public System.Random rnd = new System.Random();

    private Vector3 moveDirection;
    private bool move;

    public LayerMask mask;
    public float switchDirectionIntervall = 3.0f;
    public int maxMovesInDirectionInRow = 3;

    private int leftTicker;
    private int rightTicker;

    public override void Enter(KarenBehavior _context)
    {
        mask = _context.viewMask;
        InvokeRepeating("MoveInRandomDirection", 1.0f, switchDirectionIntervall);
    }
    private void MoveInRandomDirection()
    {
        Vector3 randomDirection;

        if (rnd.Next(2) == 0)
        {
            rightTicker++;
            randomDirection = Vector3.right;
        }
        else
        {
            leftTicker++;
            randomDirection = Vector3.left;
        }
        if(rightTicker >= maxMovesInDirectionInRow)
        {
            rightTicker = 0;
            leftTicker = 0;
            moveDirection = Vector3.left;
        }
        if (leftTicker >= maxMovesInDirectionInRow)
        {
            leftTicker = 0;
            rightTicker = 0;
            moveDirection = Vector3.right;
        }
        moveDirection = randomDirection;
    }
    public override void Do(KarenBehavior _context)
    {
        _context.rb.velocity = new Vector2(moveDirection.x * _context.walkSpeed, _context.rb.velocity.y);
        RotateLeftRight(_context);
    }
    private void RotateLeftRight(KarenBehavior _context)
    {
        if (_context.rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(_context.scale.x, _context.scale.y, _context.scale.z);
        }
        if (_context.rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-_context.scale.x, _context.scale.y, _context.scale.z);
        }
    }
    public override void FixedDo(KarenBehavior _context)
    {
    }
    public override void CheckState(KarenBehavior _context)
    {
        RaycastHit2D playerHit = Physics2D.Raycast(_context.transform.position + moveDirection, moveDirection, _context.viewDistance);
        RaycastHit2D karenHit = Physics2D.Raycast(_context.transform.position + moveDirection, moveDirection, _context.viewDistance/4);
        Debug.DrawRay(_context.transform.position + moveDirection , moveDirection * _context.viewDistance, Color.blue);
        Debug.DrawRay(_context.transform.position + moveDirection, moveDirection * _context.viewDistance/4, Color.white);
        if (playerHit.collider != null)
        {
            if (playerHit.collider.CompareTag(_context.playerTag))
            {
                Debug.DrawLine(_context.transform.position + moveDirection, playerHit.point, Color.green, 1f);
                _context.SwitchState(_context.followState);
            }
        }
        if (karenHit.collider != null)
        {
            if (karenHit.collider.CompareTag(_context.karenTag) && !karenHit.collider.gameObject.Equals(_context.gameObject))
            {
                Debug.DrawLine(_context.transform.position + moveDirection, karenHit.point, Color.red, 1f);
                moveDirection = -moveDirection;
            }
        }
    }
    public override void Exit(KarenBehavior _context)
    {
        CancelInvoke("MoveInRandomDirection");
    }

}
