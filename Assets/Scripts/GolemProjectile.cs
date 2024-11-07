using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemProjectile : MonoBehaviour
{
    private Boss golemBoss;
    public void SetGolemBoss(Boss golemBoss) => this.golemBoss = golemBoss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            golemBoss.DeductHealthPlayer(golemBoss.ProjectileDamge);
        }
    }
}
