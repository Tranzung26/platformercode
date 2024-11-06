using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossControl : MonoBehaviour
{
    public GameObject BossDoorLeft;
    public GameObject BossDoorRight;

    void Start()
    {
        GameObject boss = GameObject.FindWithTag("Boss");
        if (boss != null)
        {
            Damagable bossDamagable = boss.GetComponent<Damagable>();
            if (bossDamagable != null)
            {
                bossDamagable.Died.AddListener(OpenDoors);
            }
        }
        else
        {
            Debug.LogWarning("No Game object found with tag 'Boss'");
        }
    }

    public void CloseDoors() 
    {
        // Đóng cửa khi player vào
        BossDoorLeft.transform.position += new Vector3(0, -2, 0);//vị trí Đóng cửa
        BossDoorRight.transform.position += new Vector3(0, -2, 0);
    }

    public void OpenDoors()
    {
        // Mở cửa khi boss bị tiêu diệt
        BossDoorLeft.transform.position += new Vector3(0, 2, 0); //vị trí mở cửa
        BossDoorRight.transform.position += new Vector3(0, 2, 0);
    }
}
