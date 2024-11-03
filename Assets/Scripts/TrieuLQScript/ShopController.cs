using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    Damagable damagable = new Damagable();

    public TextMeshProUGUI CoinText;
    private Attack attack = new Attack();

    // Start is called before the first frame update
    void Start()
    {
        damagable = FindObjectOfType<Damagable>();
        attack = FindObjectOfType<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyHealItem()
    {
        int currentCoin = int.Parse(CoinText.text);
        if (currentCoin >= 1 && damagable != null)
        {
            currentCoin -= 1;
            CoinText.SetText(currentCoin.ToString());
            bool health = damagable.Heal(20);
            Debug.Log("aaaaa" + health);
        }
        else
        {
            Debug.Log("not pass condition");
        }
        
    }

    public void BuyDameItem()
    {
        int currentCoin = int.Parse(CoinText.text);
        if (currentCoin >= 2 && damagable != null)
        {
            currentCoin -= 2;
            CoinText.SetText(currentCoin.ToString());
            attack.AttackDamage += 5;
            Debug.Log("Damage boosted by 5 for 10 seconds");
            Debug.Log(attack.AttackDamage);
        }
        else
        {
            Debug.Log("not pass condition");
        }
    }

    
}
