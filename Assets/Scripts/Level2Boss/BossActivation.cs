using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivation : MonoBehaviour
{
    public BossControl bossControl;

    private bool doorsClosed = false;

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
/*        // Lưu trạng thái di chuyển hiện tại của người chơi
        bool previousCanMove = PlayerController.instance.CanMove;*/

        // Ngăn người chơi di chuyển
        PlayerController.instance.CanMove = false;

        // Đợi 3 giây
        yield return new WaitForSeconds(3f);

        // Cho phép người chơi di chuyển lại
        PlayerController.instance.CanMove = true;
    }
}
