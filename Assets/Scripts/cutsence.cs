using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutsence : MonoBehaviour
{
    public GameObject mainMenuCanvas;  // Canvas của menu chính
    public GameObject cutSceneCanvas;  // Canvas của CutScene

    void Start()
    {
        mainMenuCanvas.SetActive(false);
        cutSceneCanvas.SetActive(true);

        StartCoroutine(PlayCutSceneAndReturnToMenu());
    }

    private IEnumerator PlayCutSceneAndReturnToMenu()
    {
        yield return new WaitForSeconds(5f);

        ReturnToMenu();
    }

    public void ReturnToMenu()
    {
        cutSceneCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}
