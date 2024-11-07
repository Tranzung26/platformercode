using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject MessageUI;
    public TextMeshProUGUI MessageTxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OkAction()
    {
        MessageUI.SetActive(false);
    }

    public void BuyingSuccess()
    {
        MessageUI.SetActive(true);
        MessageTxt.text = "Success";
    }

    public void MessageError(string text)
    {
        MessageUI.SetActive(true);
        MessageTxt.text = text;
    }

    public void OpenShop()
    {
        Time.timeScale = 1;
    }


}
