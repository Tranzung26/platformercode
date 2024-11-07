using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HandleCoinCollision : MonoBehaviour
{
    public TextMeshProUGUI CoinText;
    private AudioManager audioManager;
    public GameObject saleShopUI;
    public TextMeshProUGUI priceItem;
    public TextMeshProUGUI priceDescription;

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
            priceItem.SetText("1");
            priceDescription.SetText("Max Health Item (Sale)");
            saleShopUI.SetActive(true);
            Time.timeScale = 0;
            Destroy(collision.gameObject);
        }
    }

    public void SaleShopBack()
    {
        priceItem.SetText("3");
        priceDescription.SetText("Max Health Item");
        Time.timeScale = 1;
    }

}
