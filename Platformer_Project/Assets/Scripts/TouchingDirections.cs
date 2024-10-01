using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Uses the collider to check directions to see if the object is currently on
/// the ground, touching a wall or touching a ceiling.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
public class TouchingDirections : MonoBehaviour
{
    Rigidbody2D _rb; // TODO: Do we need this? Or can it be deleted?
    CapsuleCollider2D _touchingCollider;
    RaycastHit2D[] _groundHits = new RaycastHit2D[5];
    RaycastHit2D[] _wallHits = new RaycastHit2D[5];
    RaycastHit2D[] _ceilingHits = new RaycastHit2D[5];
    Animator _animator;

    public ContactFilter2D CastFilter;


    public float GroundDistance = 0.05f;
    public float WallDistance = 0.2f;
    public float CeilingDistance = 0.05f;

    [SerializeField]
    bool _isGrounded;
    public bool IsGrounded
    {
        get => _isGrounded;
        private set
        {
            _isGrounded = value;
            _animator.SetBool(AnimationStrings.IsGrounded, value);
        }
    }

    [SerializeField]
    bool _isOnWall;
    public bool IsOnWall
    {
        get => _isOnWall;
        private set
        {
            _isOnWall = value;
            _animator.SetBool(AnimationStrings.IsOnWall, value);
        }
    }

    [SerializeField]
    bool _isOnCeiling;
    public bool IsOnCeiling
    {
        get => _isOnCeiling;
        private set
        {
            _isOnCeiling = value;
            _animator.SetBool(AnimationStrings.IsOnCeiling, value);
        }
    }

    Vector2 WallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _touchingCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        IsGrounded = _touchingCollider.Cast(Vector2.down, CastFilter, _groundHits, GroundDistance) > 0;
        IsOnWall = _touchingCollider.Cast(WallCheckDirection, CastFilter, _wallHits, WallDistance) > 0;
        IsOnCeiling = _touchingCollider.Cast(Vector2.up, CastFilter, _ceilingHits, CeilingDistance) > 0;
    }
}
