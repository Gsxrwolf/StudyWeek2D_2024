using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarenBehavior : MonoBehaviour
{
    public string playerTag = "Player";
    [HideInInspector] public GameObject player;

    [HideInInspector] public PoolSpawner spawner;

    public IdleState idleState;
    public FollowState followState;
    public AttackState attackState;
    private State curState;

    [SerializeField] private float health = 10.0f;


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

        CheckHealth();

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

    private void CheckHealth()
    {
        if(health <= 0)
        {
            Die();
        }
    }
    public void DealDamage(float _damage)
    {
        health -= _damage;
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
    }
}
