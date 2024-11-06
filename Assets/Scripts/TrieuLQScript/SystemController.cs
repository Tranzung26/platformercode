using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemController : MonoBehaviour
{
    public GameObject pauseGameUI;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseGameUI.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseGameUI.SetActive(false);
    }

    public void TryAgainAction()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevelAction()
    {
        Time.timeScale = 1;
        playerController.SavePlayerData();
        SceneManager.LoadScene("DemoMap3");
    }
}
