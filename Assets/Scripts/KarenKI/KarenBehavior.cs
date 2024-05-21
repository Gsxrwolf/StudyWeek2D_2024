using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarenBehavior : MonoBehaviour
{
    public static string playerTag = "Player";
    private GameObject player;

    public IdleState idleState;
    public FollowState followState;
    public AttackState attackState;
    private State curState;


    [SerializeField] public float speed;
    [SerializeField] public float viewDistance;
    [SerializeField] public LayerMask viewMask;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        player = GameObject.FindWithTag(playerTag);
        rb = GetComponent<Rigidbody2D>();
        curState = idleState;
        curState.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        curState.Do();
        if(curState.isComplete)
        {
            SelectState();
        }
        
    }

    private void SelectState()
    {
        curState.Enter();
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
