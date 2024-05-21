using System;
using System.Threading;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set;}
    public KarenBehavior Behavior { get; protected set; }
    protected float startTime;
    public float time => Time.time - startTime;

    protected Rigidbody2D rb;
    protected float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Behavior = GetComponent<KarenBehavior>();
        speed = Behavior.speed;
    }

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }
}