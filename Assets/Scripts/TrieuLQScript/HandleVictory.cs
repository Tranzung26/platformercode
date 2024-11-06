using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandleVictory : MonoBehaviour
{
    public GameObject gameWinUI;
    public TextMeshProUGUI CoinText;
    private bool isGameWon = false;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        isGameWon = false;
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
            Time.timeScale = 0;
            gameWinUI.SetActive(true);
            playerController.CurrentMapLevel += 1;
            playerController.SavePlayerData();

        }
        else
        {
            Debug.LogWarning("HandleCollision component not found!");
        }
    }
}

