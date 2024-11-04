using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attack : MonoBehaviour
{
    Collider2D _attackCollider; // TODO: Do we need this attack collider? It's not being used; but perhaps make sense to require it due to collision detection logic.

    public int AttackDamage = 10;

    public Vector2 KnockBack = Vector2.zero;
    private bool _isBuffed = false;
    private Coroutine _buffCoroutine;

    public TextMeshProUGUI CoinText;

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

    public void BuyDameItem()
    {
        int currentCoin = int.Parse(CoinText.text);
        if (currentCoin >= 2)
        {
            currentCoin -= 2;
            CoinText.SetText(currentCoin.ToString());
            AttackDamage += 5;
        }
        else
        {
            Debug.Log("not pass condition");
        }
    }
}
