using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Damagable _damagable;

    public TMP_Text HealthBarText;
    public Slider HealthBarSlider;
    public TextMeshProUGUI CoinText;
    public ShopController shopController;

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        Debug.Assert(player != null, "There must be an object tagged 'Player'!");
        _damagable = player.GetComponent<Damagable>();
        Debug.Assert(_damagable != null, "The object tagged 'Player' must have a 'Damagable' component!");

        //shopController = GetComponent<ShopController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        HealthBarSlider.value = CalculateSliderPercentage();
        HealthBarText.text = $"HP {_damagable.Health} / {_damagable.MaxHealth}";
    }

    private void OnEnable()
    {   
        _damagable.HealthChanged.AddListener(OnPlayerHealthChanged);
    }

   /* private void OnDisable()
    {
        _damagable.HealthChanged.RemoveListener(OnPlayerHealthChanged);
    }*/

    private float CalculateSliderPercentage() => (float)_damagable.Health / _damagable.MaxHealth;

    private void OnPlayerHealthChanged()
    {
        HealthBarSlider.value = CalculateSliderPercentage();
        HealthBarText.text = $"HP {_damagable.Health} / {_damagable.MaxHealth}";
    }

    public void BuyHealItem()
    {
        int currentCoin = int.Parse(CoinText.text);
        if (currentCoin >= 1)
        {
            currentCoin -= 1;
            CoinText.SetText(currentCoin.ToString());
            bool health = _damagable.Heal(20);
            shopController.BuyingSuccess();
        }
        else
        {
            shopController.MessageError("Don't Enough Coin. Please Try Again");
        }

    }

    public void BuyMaxHealItem()
    {
        int currentCoin = int.Parse(CoinText.text);
        if (currentCoin >= 3)
        {
            currentCoin -= 3;
            CoinText.SetText(currentCoin.ToString());
            _damagable.MaxHealth += 10;
            _damagable.Health += 10;
            shopController.BuyingSuccess();
        }
        else
        {
            shopController.MessageError("Don't Enough Coin. Please Try Again");
        }
        
        
        

    }
}
