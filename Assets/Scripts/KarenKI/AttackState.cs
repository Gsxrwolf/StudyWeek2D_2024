using UnityEngine;

public class AttackState : State
{
    private Vector3 playerPos;

    private bool attacking;

    private KarenBehavior context;

    [SerializeField] private float attackCooldown = 2;
    public float damage = 1;

    public override void Enter(KarenBehavior _context)
    {
        context = _context;

    }
    public override void Do(KarenBehavior _context)
    {
        playerPos = _context.player.transform.position;
        if (!attacking)
        {
            CheckState(_context);
            CheckHit();
        }
    }
    public void CheckHit()
    {
        attacking = true;
        RaycastHit2D hit = Physics2D.Raycast(context.transform.position + (context.sr.flipX ? Vector3.left : Vector3.right), context.sr.flipX ? Vector3.left : Vector3.right, context.viewDistance / 4f, context.viewMask);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag(context.playerTag))
            {
                Debug.Log("KarenDoDamage");
                hit.collider.gameObject.GetComponent<PlayerController>().DealDamage(damage);
            }
        }
        Invoke("EndAttack", attackCooldown);
    }

    private void EndAttack()
    {
        attacking = false;
    }
    public override void FixedDo(KarenBehavior _context)
    {
    }
    public override void CheckState(KarenBehavior _context)
    {
        if (attacking)
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
