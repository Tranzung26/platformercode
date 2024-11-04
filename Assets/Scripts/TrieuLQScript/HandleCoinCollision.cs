using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HandleCoinCollision : MonoBehaviour
{
    public TextMeshProUGUI CoinText;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CoinText.SetText(PlayerPrefs.GetInt("saveCoin").ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
	    if (collision.CompareTag("CoinTag"))
	    {
            int currentCoin = int.Parse(CoinText.text);
            audioManager.PlaySFX(audioManager.coinClip);
            currentCoin++;
            CoinText.SetText(currentCoin.ToString());
            Destroy(collision.gameObject);
	    }
    }
}
