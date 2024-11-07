using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazor : MonoBehaviour
{

    private Boss golemBoss;
    private float timer;
    private bool isPlayerInLazor = false;

    public void SetGolomBoss(Boss golemBoss) => this.golemBoss = golemBoss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            isPlayerInLazor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            isPlayerInLazor = false;
        }
    }

    private void FixedUpdate()
    {
        if (isPlayerInLazor)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= 1)
            {
                this.golemBoss.DeductHealthPlayer(golemBoss.LazerDamagePerSecond);
                timer = 0;
            }
        }
    }
}
