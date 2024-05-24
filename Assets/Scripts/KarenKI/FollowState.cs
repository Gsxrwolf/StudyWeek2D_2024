using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : State
{
    private Vector3 playerPos;

    private Vector3 moveDirection;
    public override void Enter(KarenBehavior _context)
    {
    }
    public override void Do(KarenBehavior _context)
    {
        playerPos = _context.player.transform.position;

        moveDirection = playerPos - _context.transform.position;
        
        _context.rb.velocity = new Vector2(moveDirection.x, _context.rb.velocity.y);
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
        if(Vector3.Distance(playerPos, _context.transform.position) < _context.attackRange)
        {
            _context.SwitchState(_context.attackState);
        }
        if(Vector3.Distance(playerPos, _context.transform.position) > _context.viewDistance)
        {
            _context.SwitchState(_context.idleState);
        }
    }
    public override void Exit(KarenBehavior _context)
    {
    }
}
