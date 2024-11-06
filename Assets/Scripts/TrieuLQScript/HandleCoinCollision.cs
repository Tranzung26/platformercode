using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HandleCoinCollision : MonoBehaviour
{
    public TextMeshProUGUI CoinText;
    private AudioManager audioManager;
    public GameObject saleShopUI;
    public ShopController ShopController;

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

        if (collision.CompareTag("SaleShop"))
        {
            Time.timeScale = 0;
            saleShopUI.SetActive(true);
            this.showSaleShop();
        }
    }

    private void showSaleShop()
    {
        Debug.Log("Demo");
    }
}
