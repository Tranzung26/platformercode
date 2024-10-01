using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    // TODO: Code copy of Attack with the exception of MoveSpeed. Candidate for inheritance?
    //       Notable variants:
    //          - Destroys itself on hit
    //          - Knockback from own transform.localScale.x instead of transform.parent.localScale.x

    // TODO: Projectiles need to self-destruct after some time, in order to prevent infinitely populating the game with off-screen projectiles

    Collider2D _attackCollider; // TODO: Do we need this attack collider? It's not being used; but perhaps make sense to require it due to collision detection logic.
    Rigidbody2D _rb;

    //Determines the speed of the projectile.
    public Vector2 MoveSpeed = new Vector2(3f, 0f);

    //Determines the amount of damage this projectile inflicts.
    public int Damage = 10;

    //The knockback of the attack. By default no knockback. Positive value knock away, negative values pull in.
    public Vector2 KnockBack = Vector2.zero;

    private void Awake()
    {
       // Debug.Assert(Damage > 0, "'Damage' must be greater than 0!");
        //Debug.Assert(MoveSpeed.SqrMagnitude() > 0, "'MoveSpeed' vector length must be greater than 0!");

        _rb = GetComponent<Rigidbody2D>();
        _attackCollider = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb.velocity = new Vector2(MoveSpeed.x * transform.localScale.x, MoveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Damagable>(out var damagable))
        {
            var deliveredKnockback = transform.localScale.x > 0 ?
                KnockBack :
                new Vector2(-KnockBack.x, KnockBack.y);
            bool gotHit = damagable.Hit(Damage, deliveredKnockback);
            if (gotHit)
            {
                Debug.Log($"{collision.name} hit for {Damage}");
                Destroy(gameObject);
            }
        }
    }
}
