using UnityEngine;

public class IdleState : State
{
    public System.Random rnd = new System.Random();

    private Vector3 moveDirection;
    private bool move;

    public LayerMask mask;

    public override void Enter()
    {
        mask = Behavior.viewMask;
        InvokeRepeating("MoveInRandomDirection", 0, 1);
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
    public override void Do()
    {
        rb.velocity = moveDirection * speed * Time.deltaTime;
        RaycastHit2D hit = Physics2D.Raycast(Behavior.transform.position, moveDirection, Behavior.viewDistance, mask);
        Debug.DrawLine(transform.position, hit.point, Color.red);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag(KarenBehavior.playerTag))
            {
                isComplete = true;
            }
        }
    }
    public override void FixedDo()
    {
    }
    public override void Exit()
    {
    }
}
