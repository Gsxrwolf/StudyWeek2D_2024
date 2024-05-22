using UnityEngine;

public class IdleState : State
{
    public System.Random rnd = new System.Random();

    private Vector3 moveDirection;
    private bool move;

    public LayerMask mask;

    public override void Enter(KarenBehavior _context)
    {
        mask = _context.viewMask;
        InvokeRepeating("MoveInRandomDirection", 2.0f, 1.0f);
    }
    private void MoveInRandomDirection()
    {

        Vector3 randomDirection;

        if (rnd.Next(2) == 0)
        {
            randomDirection = Vector3.right;
        }
        else
        {
            randomDirection = Vector3.left;
        }
        moveDirection = randomDirection;
    }
    public override void Do(KarenBehavior _context)
    {
        _context.rb.velocity = moveDirection * _context.speed;
        
    }
    public override void FixedDo(KarenBehavior _context)
    {
    }
    public override void CheckState(KarenBehavior _context)
    {
        RaycastHit2D hit = Physics2D.Raycast(_context.transform.position, moveDirection, _context.viewDistance, mask);
        Debug.DrawLine(transform.position, hit.point, Color.red);
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
