using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Damagable : MonoBehaviour
{
    Animator _animator;

    private float _timeSinceHit = 0;

    // TODO: Can be hit multiple times by a single attack, even when InvicibilityTimer is longer than the hit-box of the enamy attack.
    // Perhaps attacks to keep track of the attacked thing to avoid multi-hitting with a single attack?
    // Perhaps there is a problem with the animator where state-transitions allow multi-htting?
    public float InvincibilityTime = 0.25f; // In Seconds

    //Signals a damable hit has happened. First argument is the amount of damage; Second argument the knockback.
    public UnityEvent<int, Vector2> DamageableHit;

    //Signals that the health has changed.
    public UnityEvent HealthChanged;

    //Signals the death of this component.
    public UnityEvent Died;

    [SerializeField]
    int _maxHealth = 100;
    public int MaxHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    int _health = 100;
    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            HealthChanged?.Invoke(); // TODO: Assume that MaxHealth never changes during gameplay

            if (_health <= 0)
            { 
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    bool _isAlive = true;
    public bool IsAlive
    {
        get => _isAlive;
        set
        {
            _isAlive = value;
            _animator.SetBool(AnimationStrings.IsAlive, value);
            if(value == false)
            {
                Died?.Invoke();
            }
        }
    }

    [SerializeField]
    bool _isInvincible = false;
    public bool IsInvincible
    {
        get => _isInvincible;
    }

    /// <summary>
    /// Indicates that components should disallow movement input.
    /// External effects, such as knockback, can still happen.
    /// </summary>
    public bool LockVelocity
    {
        get => _animator.GetBool(AnimationStrings.LockVelocity);
        set => _animator.SetBool(AnimationStrings.LockVelocity, value);
    }

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(IsInvincible)
        {
            if (_timeSinceHit > InvincibilityTime)
            {
                _isInvincible = false;
                _timeSinceHit = 0;
            }

            _timeSinceHit += Time.deltaTime;
        }
    }

    /// <returns>True when damage was registered; False otherwise.</returns>
    public bool Hit(int damage, Vector2 knockBack)
    {
        if(IsAlive && !IsInvincible)
        {
            Health -= damage;
            _isInvincible = true;

            _animator.SetTrigger(AnimationStrings.HitTrigger);
            LockVelocity = true;
            DamageableHit?.Invoke(damage, knockBack);

            return true;
        }

        return false;
    }

    /// <returns>True when healing was registered; False otherwise.</returns>
    public bool Heal(int healthRestored)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int maxHeal = MaxHealth - Health;
            int heal = Mathf.Min(maxHeal, healthRestored);
            Health += heal;

            return true;
        }

        return false;
    }
}
