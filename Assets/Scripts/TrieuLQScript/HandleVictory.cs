using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandleVictory : MonoBehaviour
{
    public GameObject gameWinUI;
    public TextMeshProUGUI CoinText;
    private bool isGameWon = false;

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
            gameWinUI.SetActive(true);

            PlayerPrefs.SetInt("saveCoin", int.Parse(CoinText.text));
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogWarning("HandleCollision component not found!");
        }
    }
}

