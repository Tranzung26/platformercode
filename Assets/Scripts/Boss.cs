using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private enum State
    {
        Death,
        Chase,
        Patrol,
        AttackLazor,
        AttackMelee,
        AttackRange
    }

    private enum AnimatorParametor
    {
        IsWalk,
        AttackLazerStartTrigger,
        AttackLazerEndTrigger,
        AttackMeleeTrigger,
        AttackRangeTrigger,
        DeathTrigger
    }

    private State state = State.Patrol;

    [Header("Tracking Player")]
    [SerializeField]
    private float viewRange = 7f;

    [Header("Tracking Player")]
    [SerializeField]
    private float delayTrackingPlayerTime = 1f;

    [SerializeField]
    private LayerMask playerLayerMask;

    [Space]
    [SerializeField]
    private float delayCastSkillTime = 3f;

    [Header("Patrol")]
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float patrolDelay;
    [SerializeField]
    private Transform leftLimitPatrolPoint, rightLimitPatrolPoint;

    [Header("Lazer Attack")]
    [SerializeField]
    private BossLazor lazerPrefab;
    [SerializeField]
    private int lazerDamagePerSecond;
    public int LazerDamagePerSecond => lazerDamagePerSecond;
    [SerializeField]
    private Transform spawnLazerPoint;
    [SerializeField]
    private float lazorShootingTime;

    [Header("Range Attack")]
    [SerializeField]
    private BossProjectile projectilePrefab;
    [SerializeField]
    private int rangeDamage;
    public int ProjectileDamge => rangeDamage;
    [SerializeField]
    private Transform spawnProjectilePoint;

    [Header("Melee Attack")]
    [SerializeField]
    private int meleeDamage;
    [SerializeField]
    private float meleeRange;
    [SerializeField]
    private Transform meleePoint;
    [SerializeField]
    private float chaseSpeed;

    private new Rigidbody2D rigidbody2D;
    private Animator animator;
    private BoxCollider2D boxCollider2D;

    private float trackingPlayerTimer;
    private float delayCastSkillTimer;
    private float lazorShootTimer;
    private bool isSeePlayer = false;
    private Vector2 targetMovePosition;
    private Vector2 attackMeleePosition;
    private float idleTimer;
    private bool isMeleeAttacking;
    private bool isLazorAttacking;
    private bool isRangeAttacking;

    #region Draw Gizmo 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(meleePoint.position, meleeRange);
    }
    #endregion 

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        trackingPlayerTimer = delayTrackingPlayerTime;
        targetMovePosition = transform.position;
    }

    void FixedUpdate()
    {
        HandleTrackPlayer();

        switch (state)
        {
            case State.Patrol:
                HandlePatrol();
                break;
            case State.AttackMelee:
                HandleAttackMelle();
                break;

            case State.AttackLazor:
                HandleAttackLazor();
                break;

            case State.AttackRange:
                HandleAttackRange();
                break;
        }
    }

    private void HandleTrackPlayer()
    {
        trackingPlayerTimer += Time.fixedDeltaTime;
        if (trackingPlayerTimer >= delayTrackingPlayerTime)
        {
            Collider2D hit = TrackPlayer();
            isSeePlayer = hit != null;

            if (isSeePlayer)
            {
                attackMeleePosition = GetAttackMeleePosition(hit);
                //Debug.Log("See player: " + isSeePlayer);
            }
            else
            {
                //Debug.Log("Don't see player");
                if (!isLazorAttacking && !isRangeAttacking) ChangeState(State.Patrol);
            }

            trackingPlayerTimer = 0;
        }
        if (isSeePlayer)
        {
            HandleSwitchSkill();
        }
    }

    private Collider2D TrackPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, viewRange, playerLayerMask);
        return hit;
    }

    private void HandleSwitchSkill()
    {
        delayCastSkillTimer += Time.fixedDeltaTime;
        if (delayCastSkillTimer >= delayCastSkillTime && state == State.Patrol)
        {
            int randomSkill = UnityEngine.Random.Range(0, 3);
            if (randomSkill == 0)
            {
                ChangeState(State.AttackMelee);
                animator.SetBool(AnimatorParametor.IsWalk.ToString(), true);
            }
            if (randomSkill == 1)
            {
                ChangeState(State.AttackLazor);
            }
            if (randomSkill == 2) ChangeState(State.AttackRange);
            Debug.Log("Cast skill: " + state.ToString());
        }
    }

    private void ChangeState(State state)
    {
        if (state == this.state) return;
        Debug.Log($"Change state {this.state.ToString()} to state {state.ToString()}");
        this.state = state;
    }

    #region Patrol
    private void HandlePatrol()
    {
        idleTimer += Time.fixedDeltaTime;
        if (idleTimer >= patrolDelay)
        {
            bool isMoveToTargetPos = Move(targetMovePosition, moveSpeed);
            animator.SetBool(AnimatorParametor.IsWalk.ToString(), true);

            if (isMoveToTargetPos)
            {
                idleTimer = 0;
            }
        }
        else
        {
            animator.SetBool(AnimatorParametor.IsWalk.ToString(), false);

            float xRandom = Random.Range(leftLimitPatrolPoint.position.x, rightLimitPatrolPoint.position.x);
            targetMovePosition.x = xRandom;
        }
    }
    #endregion

    #region Melee Attack
    private Vector3 GetAttackMeleePosition(Collider2D hit)
    {
        var attackRange = Vector2.Distance(transform.position, meleePoint.position);
        Vector2 attackPos = hit.transform.position;
        var vectorDirection = (Vector2)hit.transform.position - (Vector2)transform.position;
        if (vectorDirection.x < 0)
        {
            attackPos.x += attackRange;
        }
        else
        {
            attackPos.x -= attackRange;
        }
        return attackPos;
    }

    private void HandleAttackMelle()
    {
        if (isMeleeAttacking) return;
        bool isMoveToMeleePosition = Move(attackMeleePosition, chaseSpeed);
        if (isMoveToMeleePosition)
        {
            animator.SetTrigger(AnimatorParametor.AttackMeleeTrigger.ToString());
            isMeleeAttacking = true;
        }
    }

    public void TraceMelleDamage()
    {
        Collider2D hit = Physics2D.OverlapCircle(meleePoint.position, meleeRange, playerLayerMask);
        if (hit != null)
        {
            DeductHealthPlayer(meleeDamage);
        }
        else
        {
            Debug.Log("Don't hit player");
        }
    }

    public void EndMeleeAttack()
    {
        isMeleeAttacking = false;
        CombackToPatrol();
    }
    #endregion

    private void CombackToPatrol()
    {
        delayCastSkillTime = 0;
        ChangeState(State.Patrol);
        animator.SetBool(AnimatorParametor.IsWalk.ToString(), false);
    }

    #region Lazor Attack
    private void HandleAttackLazor()
    {
        if (isLazorAttacking) return;
        Vector2 shootDirection = attackMeleePosition - (Vector2)transform.position;
        if ((shootDirection.x < 0 && transform.localScale.x > 0) || (shootDirection.x > 0 && transform.localScale.x < 0))
        {
            Flip();
        }
        animator.SetTrigger(AnimatorParametor.AttackLazerStartTrigger.ToString());
        isLazorAttacking = true;
    }

    public void SpawnLazor()
    {
        BossLazor bossLazor = Instantiate(lazerPrefab, spawnLazerPoint.position, Quaternion.identity);
        bossLazor.StartShoot(this, lazorShootingTime);
    }

    public void EndLazorAttack()
    {
        isLazorAttacking = false;
        animator.SetTrigger(AnimatorParametor.AttackLazerEndTrigger.ToString());
        CombackToPatrol();
    }
    #endregion

    #region Range Attack
    private void HandleAttackRange()
    {
        if (isRangeAttacking) return;
        Vector2 shootDirection = attackMeleePosition - (Vector2)transform.position;
        if ((shootDirection.x < 0 && transform.localScale.x > 0) || (shootDirection.x > 0 && transform.localScale.x < 0))
        {
            Flip();
        }
        animator.SetTrigger(AnimatorParametor.AttackRangeTrigger.ToString());
        isRangeAttacking = true;
    }


    public void SpawnProjectile()
    {
        BossProjectile bossProjectile = Instantiate(projectilePrefab, spawnProjectilePoint.position, Quaternion.identity);
        bossProjectile.SetPosition(spawnProjectilePoint.position);
        bossProjectile.SetMoveDirection(transform.localScale.x > 0 ? Vector2.right : Vector2.left);
        bossProjectile.SetGolemBoss(this);
    }

    public void EndAttackRange()
    {
        isRangeAttacking = false;
        CombackToPatrol();
    }
    #endregion

    public void DeductHealthPlayer(int damage) // Need to add Player Health Component
    {
        Debug.Log("Deduct Health Player: " + damage);
    }

    private bool Move(Vector2 position, float moveSpeed)
    {
        if (Vector2.Distance((Vector2)transform.position, position) <= 0.2f) return true;
        Vector2 direction = (position - (Vector2)transform.position).normalized;

        transform.position = Vector2.MoveTowards(transform.position, position, moveSpeed * Time.fixedDeltaTime);

        if (direction.x > 0 && transform.localScale.x < 0)
        {
            Flip();
        }
        else if (direction.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
        return false;
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
