using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class Attack : MonoBehaviour
{
    Animator _animator;
    Collider2D _attackCollider;

    public int AttackDamage = 10;
    public int ReducedAttackDamage = 5; // Sát thương khi máu thấp
    public float IncreasedAttackSpeed = 1.5f; // Tốc độ đánh khi máu thấp
    private float _originalAttackSpeed = 1.0f; // Tốc độ đánh ban đầu
    public float AttackCooldown = 1.0f;
    private float _currentAttackCooldown = 0f;
    public Vector2 KnockBack = Vector2.zero;
    private bool _isBuffed = false;
    private Coroutine _buffCoroutine;

    public TextMeshProUGUI CoinText;
    public ShopController shopController;
    public SpriteRenderer characterSpriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _attackCollider = GetComponent<Collider2D>();
        _originalAttackSpeed = _animator.speed; // Lưu tốc độ ban đầu
    }

    private void Update()
    {
        if (_currentAttackCooldown > 0)
        {
            _currentAttackCooldown -= Time.deltaTime;
        }
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
            characterSpriteRenderer.color = Color.red;
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
        characterSpriteRenderer.color = Color.white;

        Debug.Log("Damage buff ended");
    }

    public void PerformAttack()
    {
        if (_currentAttackCooldown <= 0)
        {
            _animator.SetTrigger(AnimationStrings.AttackTrigger);
            _currentAttackCooldown = AttackCooldown;
        }
    }

    // Thêm phương thức AdjustForLowHealth
    public void AdjustForLowHealth()
    {
        AttackDamage = ReducedAttackDamage;
        _animator.speed = IncreasedAttackSpeed;
        Debug.Log("Adjusted damage and speed for low health.");
    }

    public void BuyDameItem()
    {
        int currentCoin = int.Parse(CoinText.text);
        if (currentCoin >= 2)
        {
            currentCoin -= 2;
            CoinText.SetText(currentCoin.ToString());
            AttackDamage += 5;
            shopController.BuyingSuccess();
        }
        else
        {
            shopController.MessageError("Don't Enough Coin. Please Try Again");
        }
    }
}
