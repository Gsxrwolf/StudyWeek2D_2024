using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackState : State
{
    private Vector3 playerPos;

    private bool attacking;

    private KarenBehavior context;

    public float damage;

    public override void Enter(KarenBehavior _context)
    {
        context = _context;
        attacking = true;
    }
    public override void Do(KarenBehavior _context)
    {
        Debug.Log("Attack");
        playerPos = _context.player.transform.position;
    }
    public void AttackEnd()
    {
        attacking = false;
    }
    public void CheckHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(context.transform.position, context.sr.flipX? Vector3.left : Vector3.right , context.viewDistance, context.viewMask);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag(context.playerTag))
            {
                Debug.Log("Damage");
                //hit.collider.gameObject.GetComponent<PlayerController>().DealDamage(damage);
            }
        }
    }
    public override void FixedDo(KarenBehavior _context)
    {
    }
    public override void CheckState(KarenBehavior _context)
    {
        if(attacking)
        {
            return;
        }
        if (Vector3.Distance(playerPos, _context.transform.position) > _context.attackRange)
        {
            _context.SwitchState(_context.followState);
        }
        if (Vector3.Distance(playerPos, _context.transform.position) > _context.viewDistance)
        {
            _context.SwitchState(_context.idleState);
        }
    }
    public override void Exit(KarenBehavior _context)
    {
    }
}
