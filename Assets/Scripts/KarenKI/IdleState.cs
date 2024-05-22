using UnityEngine;

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
        Debug.Log("Idle");
        _context.rb.velocity = moveDirection * _context.walkSpeed;
    }
    public override void FixedDo(KarenBehavior _context)
    {
    }
    public override void CheckState(KarenBehavior _context)
    {
        RaycastHit2D hit = Physics2D.Raycast(_context.transform.position, moveDirection, _context.viewDistance, mask);
        Debug.DrawLine(transform.position, hit.point, Color.red);
        Debug.DrawRay(transform.position, hit.point, Color.green);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag(_context.playerTag))
            {
                _context.SwitchState(_context.followState);
            }
        }
    }
    public override void Exit(KarenBehavior _context)
    {
    }

}
