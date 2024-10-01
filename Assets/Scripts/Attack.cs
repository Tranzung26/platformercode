using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attack : MonoBehaviour
{
    Collider2D _attackCollider; // TODO: Do we need this attack collider? It's not being used; but perhaps make sense to require it due to collision detection logic.

    public int AttackDamage = 10;

    public Vector2 KnockBack = Vector2.zero;

    private void Awake()
    {
        _attackCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Damagable>(out var damagable))
        {
            var deliveredKnockback = transform.parent.localScale.x > 0 ?
                KnockBack :
                new Vector2(-KnockBack.x, KnockBack.y);
            bool gotHit = damagable.Hit(AttackDamage, deliveredKnockback);
            if(gotHit)
            {
                Debug.Log($"{collision.name} hit for {AttackDamage}");
            }
        }
    }
}
