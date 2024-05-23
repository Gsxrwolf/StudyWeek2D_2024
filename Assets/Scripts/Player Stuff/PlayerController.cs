using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Editor Settings
    [SerializeField] private float _speed = 100;
    [SerializeField] private float _jumpForce = 10;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _spriteObject = null;
    [SerializeField] private SpriteRenderer _spriteRenderer = null;

    // Private Values
    private float _horizontal;
    private float _rayLength;
    private CapsuleCollider2D _collider = null;
    private bool _grounded = false;
    private float _prevHorizontal;
    private bool _cooldown;





    /*
    public float xRange = 10.0f;
    public float horizontalInput;

    [Range(1, 50)] public float health = 45;
    [SerializeField] public float speed = 5.0f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 5.0f;
    private float rayLength;
    [SerializeField] private bool grounded = true;
    [SerializeField] private bool hasJumped = true;
    [SerializeField] private LayerMask groundLayers;
    */



    #region Events

    void Start()
    {
        // Gte collider
        _collider = _spriteObject.GetComponent<CapsuleCollider2D>();

        // Create collider if invalid
        if (_collider is null)
        {
            _collider = _spriteObject.AddComponent<CapsuleCollider2D>();
        }

        // Calc ray length
        _rayLength = (_collider.size.y / 2) + (_collider.size.y / 100);

        // Start background routine
        StartCoroutine(GroundCheck());
    }

    private void FixedUpdate()
    {
        // Move Player
        _rigidbody.velocity = new Vector2((_horizontal * (_speed * 2)) * Time.deltaTime, _rigidbody.velocity.y);
    }

    #endregion

    #region Utility

    private bool CheckGrounded()
    {
        // Ray to ground. Make sure player is not aired
        var hitInfo = Physics2D.Raycast(transform.position, Vector2.down, _rayLength, _layerMask);

        Debug.DrawRay(this.gameObject.transform.position, Vector2.down, Color.red);
        return hitInfo.collider;
    }

    #endregion

    #region Player Inputs

    public void Jump(InputAction.CallbackContext context)
    {
        // Perform on input start
        if (context.started)
        {
            // Check if char is on groúnd
            if (_grounded)
            {
                // Apply jump force
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        // Get input on X only and set in private var
        _horizontal = context.ReadValue<Vector2>().x;




        // If the player moves to the right
        if(_horizontal == 1.0f)
        {
            // No Flip
            _spriteRenderer.flipY = false;
            return;
        }

        // if the player moves to the left
        if(_horizontal == -1.0f)
        {
            // Flip
            _spriteRenderer.flipY = true;
            return;
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if(_cooldown is false)
        {
            Debug.Log("Attack");
            StartCoroutine(AttackCooldown());
        }
    }

    #endregion


    #region Routines

    /// <summary>
    /// Will run in the background and checks if the player is aired or not
    /// </summary>
    /// <returns></returns>
    private IEnumerator GroundCheck()
    {
        // Internal field -> The players original gravity scale
        float originalScale = _rigidbody.gravityScale;

        // Loop as long as class exists
        while (true)
        {

            if (CheckGrounded())
            {
                _rigidbody.gravityScale = originalScale;
                _grounded = true;
            }
            else
            {
                _rigidbody.gravityScale = originalScale * 2;
                _grounded = false;
            }

            // Wait for end of frame
            yield return null;
        }
    }


    private IEnumerator AttackCooldown()
    {
        _cooldown = true;

        yield return new WaitForSeconds(3);

        _cooldown = false;
    }

    #endregion
}
