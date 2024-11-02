using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandleVictory : MonoBehaviour
{
    public GameObject gameWinUI;
    private bool isGameWon = false;
    private HandleCoinCollision handleCoinCollision;

    // Start is called before the first frame update
    void Start()
    {
        isGameWon = false;
        // Tìm component HandleCollision trong scene
        handleCoinCollision = FindObjectOfType<HandleCoinCollision>(); // tham chiếu đến HandleCoinCollision để có thể lấy được data current
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isGameWon)
        {
            isGameWon = true;
            gameWinUI.SetActive(true);

            if (handleCoinCollision != null)
            {
                PlayerPrefs.SetInt("saveCoin", handleCoinCollision.Coin);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.LogWarning("HandleCollision component not found!");
            }
        }
    }
}
