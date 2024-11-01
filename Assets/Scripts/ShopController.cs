using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    Damagable damagable = new Damagable();
    HealthBar healthBar = new HealthBar();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyHealItem()
    {

        Debug.Log("aaaaaaaaaa");
        Debug.Log("bbbbbbbb" + damagable.MaxHealth);
        damagable.MaxHealth += 10;
        damagable.Health += 10;
        Debug.Log("ccccccc" + damagable.MaxHealth);
        
    }
}
