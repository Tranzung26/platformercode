using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TouchingDirections))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Damagable))]
public class Knight2 : MonoBehaviour
{
    public float WalkSpeed = 3f;
    public DetectionZone AttackZone;
    public DetectionZone CliffDetectionZone;
    public float WalkStopRate = 0.05f;

    public int XPGainOnDeath = 50;
    private Damagable _damagable;

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

    // Thêm giới hạn vùng di chuyển
    public float leftBoundary;
    public float rightBoundary;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _touchingDirections = GetComponent<TouchingDirections>();
        _animator = GetComponent<Animator>();
        _damageable = GetComponent<Damagable>();

        // Thiết lập ranh giới cố định
        leftBoundary = transform.position.x - 5f;
        rightBoundary = transform.position.x + 5f;
        _damagable = GetComponent<Damagable>();
        _damagable.Died.AddListener(OnDeath);
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

    private void OnDeath()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.GainExperience(XPGainOnDeath);
        }
    }

    void HandleMovement()
    {
        // Kiểm tra giới hạn ranh giới
        if (transform.position.x <= leftBoundary && WalkDirection == WalkableDirection.Left)
        {
            FlipDirection();
        }
        else if (transform.position.x >= rightBoundary && WalkDirection == WalkableDirection.Right)
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
