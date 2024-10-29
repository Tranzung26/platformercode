using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HandleCollision : MonoBehaviour
{
    public int Coin = 0;
    public TextMeshProUGUI CoinText;

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
            Coin++;
            PlayerPrefs.SetInt("saveCoin", Coin);
            CoinText.SetText(Coin.ToString());
            Destroy(collision.gameObject);
	    }
    }
    public void SaveCoin()
    {
        PlayerPrefs.SetInt("saveCoin", 10);
    }
}
