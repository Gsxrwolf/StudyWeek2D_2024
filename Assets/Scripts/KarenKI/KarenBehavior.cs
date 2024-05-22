using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarenBehavior : MonoBehaviour
{
    public string playerTag = "Player";
    [HideInInspector] public GameObject player;

    public IdleState idleState;
    public FollowState followState;
    public AttackState attackState;
    private State curState;


    [SerializeField] public float walkSpeed;
    [SerializeField] public float viewDistance;
    [SerializeField] public LayerMask viewMask;
    [SerializeField] public float attackRange;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer sr;

    void Start()
    {
        player = GameObject.FindWithTag(playerTag);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();


        curState = idleState;
        curState.Enter(this);
    }

    void Update()
    {
        curState.Do(this);
        curState.CheckState(this);

        RotateLeftRight();
    }

    public void SwitchState(State _newState)
    {
        curState.Exit(this);
        curState = _newState;
        curState.Enter(this);
    }

    private void RotateLeftRight()
    {
        if (rb.velocity.x > 0)
        {
            sr.flipX = true;
        }
        if (rb.velocity.x < 0)
        {
            sr.flipX = false;
        }
    }
}
