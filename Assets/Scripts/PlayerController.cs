using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(TouchingDirections))]
[RequireComponent(typeof(Damagable))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    bool _isMoving = false;
    [SerializeField]
    bool _isRunning = false;
    bool _isFacingRight = true;

    Vector2 _moveInput;
    Rigidbody2D _rb;
    Animator _animator;
    TouchingDirections _touchingDirections;
    Damagable _damagable;

    //Determines the walk-speed of the player.
    public float WalkSpeed = 5f;
   //"Determines the run-speed of the player.
    public float RunSpeed = 8f;
    //Determines the strength of the jump.
    public float JumpImpulse = 10f;
    //Determines the lateral move-speed of the player while in the air.
    public float AirwalkSpeed = 3f;

    private Vector3 respawnPoint;
    public GameObject fallDetector;

    private void Start()
    {
        respawnPoint = transform.position;
        _damagable = GetComponent<Damagable>();
    }

    public bool IsMoving
    {
        get => _isMoving;
        private set
        {
            _isMoving = value;
            _animator.SetBool(AnimationStrings.IsMoving, value);
        }
    }

    public bool IsRunning
    {
        get => _isRunning;
        private set
        {
            _isRunning = value;
            _animator.SetBool(AnimationStrings.IsRunning, value);
        }
    }

    public bool CanMove => _animator.GetBool(AnimationStrings.CanMove);

    float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !_touchingDirections.IsOnWall)
                {
                    if (_touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return RunSpeed;
                        }
                        else
                        {
                            return WalkSpeed;
                        }
                    }
                    else
                    {
                        // TODO: Keep initial lateral velocity on jump. When switching directions, then keep using AirwalkSpeed.
                        // We're in the air:
                        return AirwalkSpeed;
                    }
                }
                else
                {
                    // idle speed:
                    return 0;
                }
            }
            else
            {
                // Player is not allowed to move:
                return 0;
            }
        }
    }
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _touchingDirections = GetComponent<TouchingDirections>();
        _damagable = GetComponent<Damagable>();
    }

    private void Update()
    {
        if (CanMove && !_damagable.LockVelocity)
        {
            MovePlayer();
        }
        
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    private void MovePlayer()
    {
        // Only control lateral movement from Player Input:
        Vector2 targetVelocity = new Vector2(_moveInput.x * CurrentMoveSpeed, _rb.velocity.y);
        _rb.velocity = targetVelocity;

        _animator.SetFloat(AnimationStrings.YVelocity, _rb.velocity.y);

        if (_moveInput.x > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (_moveInput.x < 0 && _isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        _isMoving = _moveInput != Vector2.zero;
        _animator.SetBool(AnimationStrings.IsMoving, _isMoving);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        _isRunning = context.ReadValueAsButton();
        _animator.SetBool(AnimationStrings.IsRunning, _isRunning);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (CanMove && context.started && _touchingDirections.IsGrounded)
        {
            _animator.SetTrigger(AnimationStrings.JumpTrigger);
            _rb.velocity = new Vector2(_rb.velocity.x, JumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _animator.SetTrigger(AnimationStrings.AttackTrigger);
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _animator.SetTrigger(AnimationStrings.RangedAttackTrigger);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        _rb.velocity = new Vector2(knockback.x, _rb.velocity.y + knockback.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;

            // Deduct 5 HP when respawning
            if (_damagable != null)
            {
                _damagable.Health -= 5;
            }
        }
        else if(collision.tag == "CheckPoint")
        {
            respawnPoint = transform.position;
        }
    }
}
