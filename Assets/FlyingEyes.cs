using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Damagable))]
public class FlyingEyes : MonoBehaviour
{
    Rigidbody2D _rb;
    Animator _animator;
    Damagable _damageable;

    [Tooltip("The script managing the detection for triggering bite attacks.")]
    public DetectionZone BiteDetectionZone;

    [Tooltip("The fixed distance the Flying Eye moves from its initial position.")]
    public float moveDistance = 5f;  // regions

    [Tooltip("Determines the flight speed.")]
    public float FlightSpeed = 2f;

    [Tooltip("The collider to be enabled on death.")]
    public Collider2D DeathCollider;

    [Tooltip("The amount of experience gained when the Flying Eye dies.")]
    public int XPGainOnDeath1 = 30; // Lượng XP trao cho người chơi

    private Vector3 initialPosition;  // First Location
    private bool movingRight = true;  

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

    void Awake()
    {
        Debug.Assert(moveDistance > 0, "'moveDistance' must be greater than 0!");
        Debug.Assert(BiteDetectionZone != null, "'BiteDetectionZone' must be set!");
        Debug.Assert(FlightSpeed > 0, "'FlightSpeed' must be greater than 0!");
        Debug.Assert(DeathCollider != null, "'DeathCollider' must be set!");
        Debug.Assert(DeathCollider.enabled == false, "'DeathCollider' must be disabled on awake!");

        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _damageable = GetComponent<Damagable>();

        // Save first locations
        initialPosition = transform.position;
    }

    void Update()
    {
        HasTarget = BiteDetectionZone.DetectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (_damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                _rb.velocity = Vector2.zero;
            }
        }
        else
        {
            _rb.gravityScale = 2f;
            _rb.velocity = new Vector2(0, _rb.velocity.y);
            DeathCollider.enabled = true;
        }
    }

    private void Flight()
    {
        float direction = movingRight ? 1f : -1f;
        _rb.velocity = new Vector2(direction * FlightSpeed, _rb.velocity.y);
        UpdateDirection();

        // check region
        if (movingRight && transform.position.x >= initialPosition.x + moveDistance)
        {
            movingRight = false;
        }
        else if (!movingRight && transform.position.x <= initialPosition.x - moveDistance)
        {
            movingRight = true;
        }
    }

    private void UpdateDirection()
    {
        Vector3 localScale = transform.localScale;
        //  flip moverment
        if (movingRight && localScale.x < 0)
        {
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z); // right
        }
        else if (!movingRight && localScale.x > 0)
        {
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z); // left
        }
    }

    private void OnEnable()
    {
        _damageable.Died.AddListener(OnDeath);
    }

    private void OnDisable()
    {
        _damageable.Died.RemoveListener(OnDeath);
    }

    public void OnDeath()
    {
        _rb.gravityScale = 2f;
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        DeathCollider.enabled = true;

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.GainExperience(XPGainOnDeath1);
        }
    }
}
