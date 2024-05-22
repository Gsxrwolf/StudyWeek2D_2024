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
        Debug.Log("Follow");
        playerPos = _context.player.transform.position;

        moveDirection = playerPos - _context.transform.position;
        
        _context.rb.velocity = moveDirection;
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
