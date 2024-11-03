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
    private int CurrentCoin = 0;

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        Debug.Assert(player != null, "There must be an object tagged 'Player'!");
        _damagable = player.GetComponent<Damagable>();
        Debug.Assert(_damagable != null, "The object tagged 'Player' must have a 'Damagable' component!");
    }

    // Start is called before the first frame update
    void Start()
    {
        HealthBarSlider.value = CalculateSliderPercentage();
        HealthBarText.text = $"HP {_damagable.Health} / {_damagable.MaxHealth}";
        CurrentCoin = PlayerPrefs.GetInt("saveCoin");
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

    public void BuyMaxHealItem()
    {
        Debug.Log("aaaaaaaaaaa" + CurrentCoin);
        if (CurrentCoin >= 10)
        {
            CurrentCoin -= 10;
            CoinText.SetText(CurrentCoin.ToString());
            PlayerPrefs.SetInt("saveCoin", CurrentCoin);
            PlayerPrefs.Save();
            _damagable.MaxHealth += 10;
            _damagable.Health += 10;
        }

        Debug.Log("bbbbbbbbb" + PlayerPrefs.GetInt("saveCoin"));
    }
}
