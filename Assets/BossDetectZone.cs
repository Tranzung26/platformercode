using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDetectZone : MonoBehaviour
{
    private Transform playerTransform; // Lưu trữ Transform của Player khi vào vùng

    public bool PlayerInRange => playerTransform != null; // Kiểm tra xem Player có trong vùng không

    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerTransform = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerTransform = null;
        }
    }
}
