using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivation : MonoBehaviour
{
    public BossControl bossControl;
    private BringerofDead lastBoss;
    private bool doorsClosed = false;

    void Start()
    {
        // Tìm đối tượng với tag "Boss" và lấy tham chiếu đến Knight2
        GameObject boss = GameObject.FindWithTag("Boss");
        if (boss != null)
        {
            lastBoss = boss.GetComponent<BringerofDead>(); 
        }
        else
        {
            Debug.LogWarning("No GameObject found with tag 'Boss'");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !doorsClosed)
        {
            bossControl.CloseDoors();
            doorsClosed = true;
            StartCoroutine(WaitForBoss());
        }
    }

    IEnumerator WaitForBoss()
    {
        // Ngăn boss di chuyển nếu bossKnight đã được tìm thấy
        if (lastBoss != null)
        {
            lastBoss.CanMove = false;
        }

        // Ngăn người chơi di chuyển
        PlayerController.instance.CanMove = false;

        // Đợi 3 giây
        yield return new WaitForSeconds(3f);

        // Cho phép người chơi di chuyển lại
        PlayerController.instance.CanMove = true;

        // Cho phép boss di chuyển lại nếu bossKnight đã được tìm thấy
        if (lastBoss != null)
        {
            lastBoss.CanMove = true;
        }
    }
}
