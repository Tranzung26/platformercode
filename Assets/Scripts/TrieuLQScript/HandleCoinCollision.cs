using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HandleCoinCollision : MonoBehaviour
{
    public int Coin = 0;
    public TextMeshProUGUI CoinText;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Coin = PlayerPrefs.GetInt("saveCoin");
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
            audioManager.PlaySFX(audioManager.coinClip);
            Coin++;
            //PlayerPrefs.SetInt("saveCoin", Coin); // check if here
            CoinText.SetText(Coin.ToString());
            Destroy(collision.gameObject);
	    }
    }
}
