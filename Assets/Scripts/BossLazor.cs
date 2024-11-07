using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLazor : MonoBehaviour
{

    [SerializeField]
    private Lazor lazor;

    private float timeShoot;
    private float shootTimer;
    private Boss golemBoss;

    public void StartShoot(Boss golemBoss, float timeShoot)
    {
        this.timeShoot = timeShoot;
        this.golemBoss = golemBoss;
        lazor.SetGolomBoss(golemBoss);

        if (golemBoss.transform.localScale.x < 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void FixedUpdate()
    {
        shootTimer += Time.fixedDeltaTime;
        if (shootTimer >= timeShoot)
        {
            EndShoot();
        }
    }

    private void EndShoot()
    {
        golemBoss.EndLazorAttack();
        Destroy(gameObject);
    }
}
