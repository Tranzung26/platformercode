using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TouchingDirections))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Damagable))]
public class Knight : MonoBehaviour
{
    //Determines the walkspeed of the knight
    public float WalkSpeed = 3f;

    //The script managing the detection for triggering attacks
    public DetectionZone AttackZone;
    //The script managing the detection of cliffs
    public DetectionZone CliffDetectionZone;

    //Determines the rate at which the knight slows down it's lateral movement on attack
    public float WalkStopRate = 0.05f;

    Rigidbody2D _rb;
    TouchingDirections _touchingDirections;
    Animator _animator;
    Damagable _damageable;

    public enum WalkableDirection { Right, Left };

    Vector2 _walkDirectionVector = Vector2.right;
    WalkableDirection _walkDirection;
    public WalkableDirection WalkDirection
    {
        get => _walkDirection;
        set
        {
            if (_walkDirection != value)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

                _walkDirectionVector = value == WalkableDirection.Right ? Vector2.right : Vector2.left;
            }
            _walkDirection = value;
        }
    }

    private bool _hasTarget = false;
    public bool HasTarget
    {
        get => _hasTarget;
        private set
        {
            _hasTarget = value;
            _animator.SetBool(AnimationStrings.HasTarget, value);
        }
    }

    public bool CanMove => _animator.GetBool(AnimationStrings.CanMove);

    public float AttackCooldown
    {
        get => _animator.GetFloat(AnimationStrings.AttackCooldown);
        private set => _animator.SetFloat(AnimationStrings.AttackCooldown, Mathf.Max(value, 0));
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _touchingDirections = GetComponent<TouchingDirections>();
        _animator = GetComponent<Animator>();
        _damageable = GetComponent<Damagable>();
    }

    void Update()
    {
        HasTarget = AttackZone.DetectedColliders.Count > 0;

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }

        HandleMovement();
    }

    void HandleMovement()
    {
        if (_touchingDirections.IsGrounded && _touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        if (!_damageable.LockVelocity)
        {
            if (CanMove && _touchingDirections.IsGrounded)
            {
                _rb.velocity = new Vector2(WalkSpeed * _walkDirectionVector.x, _rb.velocity.y);
            }
            else
            {
                _rb.velocity = new Vector2(Mathf.Lerp(_rb.velocity.x, 0, WalkStopRate), _rb.velocity.y);
            }
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else
        {
            WalkDirection = WalkableDirection.Right;
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        _rb.velocity = new Vector2(knockback.x, _rb.velocity.y + knockback.y);
    }

    public void OnCliffDetected()
    {
        if (_touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
}
