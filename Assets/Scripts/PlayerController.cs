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

    public bool IsMoving // variable
    {
        get => _isMoving;
        private set
        {
            _isMoving = value;
            _animator.SetBool(AnimationStrings.IsMoving, value);
        }
    }

    public bool IsRunning // variable
    {
        get => _isRunning;
        private set
        {
            _isRunning = value;
            _animator.SetBool(AnimationStrings.IsRunning, value);
        }
    }

    public bool CanMove => _animator.GetBool(AnimationStrings.CanMove); // variable

    float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !_touchingDirections.IsOnWall) // check chạm tường => speed 0
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

    

    private void Awake() // init
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _touchingDirections = GetComponent<TouchingDirections>();
        _damagable = GetComponent<Damagable>();
    }

    private void Update() // event bắt action to update object
    {
        if (CanMove && !_damagable.LockVelocity) // LockVelocity lock di chuyển
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        // Only control lateral movement from Player Input:
        Vector2 targetVelocity = new Vector2(_moveInput.x * CurrentMoveSpeed, _rb.velocity.y); // hander speed ngang, no change dọc
        _rb.velocity = targetVelocity;

        _animator.SetFloat(AnimationStrings.YVelocity, _rb.velocity.y); // check action player

        if (_moveInput.x > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (_moveInput.x < 0 && _isFacingRight)
        {
            Flip();
        }
    }

    private void Flip() // xoay người
    {
        _isFacingRight = !_isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // đảo ngược giá trị của trục x
        transform.localScale = scale;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>(); // lấy giá trị Vector2 để move
        //_moveInput = new Vector2(1, 0); // qua phải
        //Debug.Log(context.ReadValue<Vector2>()); 
        _isMoving = _moveInput != Vector2.zero;
        _animator.SetBool(AnimationStrings.IsMoving, _isMoving);
    }

    //bool IsFacingRight // variable
    //{
    //    get => _isFacingRight;
    //    set
    //    {
    //        if (_isFacingRight != value)
    //        {
    //            // Flip x-orientation:
    //            transform.localScale *= new Vector2(-1, 1);
    //        }
    //        _isFacingRight = value;
    //    }
    //}

    //bool IsAlive => _animator.GetBool(AnimationStrings.IsAlive); // variable

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
        _rb.velocity = new Vector2(knockback.x, knockback.y);
    }
}
