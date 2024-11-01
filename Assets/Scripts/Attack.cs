using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attack : MonoBehaviour
{
    Collider2D _attackCollider; // TODO: Do we need this attack collider? It's not being used; but perhaps make sense to require it due to collision detection logic.

    public int AttackDamage = 10;
    public int DamagePerLevel = 5; // Amount to increase damage per level

    public Vector2 KnockBack = Vector2.zero;
    private bool _isBuffed = false;
    private Coroutine _buffCoroutine;

    private void Awake()
    {
        _attackCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Damagable>(out var damagable))
        {
            var deliveredKnockback = transform.parent.localScale.x > 0 ?
                KnockBack :
                new Vector2(-KnockBack.x, KnockBack.y);
            bool gotHit = damagable.Hit(AttackDamage, deliveredKnockback);
            if (gotHit)
            {
                Debug.Log($"{collision.name} hit for {AttackDamage}");
            }
        }
    }
    public void BoostDamage(int boostAmount, float duration)
    {
        if (!_isBuffed)
        {
            _isBuffed = true;
            AttackDamage += boostAmount;


            _buffCoroutine = StartCoroutine(RemoveBuff(boostAmount, duration));
        }
    }

    private IEnumerator RemoveBuff(int boostAmount, float duration)
    {
        yield return new WaitForSeconds(duration);

        AttackDamage -= boostAmount;
        _isBuffed = false;

        Debug.Log("Damage buff ended");
    }

    private void Start()
    {
        // Get PlayerController reference and subscribe to OnLevelUp
        var playerController = GetComponentInParent<PlayerController>();
        if (playerController != null)
        {
            playerController.OnLevelUp += HandleLevelUp;
        }
    }

    private void HandleLevelUp(int newLevel)
    {
        // Increase damage by DamagePerLevel for each level up
        AttackDamage += DamagePerLevel;
        Debug.Log($"Damage increased to {AttackDamage} at Level {newLevel}");
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        var playerController = GetComponentInParent<PlayerController>();
        if (playerController != null)
        {
            playerController.OnLevelUp -= HandleLevelUp;
        }
    }
}
