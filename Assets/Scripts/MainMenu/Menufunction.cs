using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private int currentMapLevel;
    public GameObject messageUI;
    void Start()
    {
        currentMapLevel = PlayerPrefs.GetInt("CurrentMapLevel");
    }
    public void Level1()
    {
        if (currentMapLevel > 1)
        {
            messageUI.SetActive(true);
        }
        else
        {
            PlayNewGame();
        }
    }

    public void Level2()
    {
        SceneManager.LoadScene("DemoMap3");
    }

    public void Level3()
    {
        Debug.Log("Level 3");
    }

    public void PlayNewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("GameplayScene");
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
       Application.Quit();
    }


}
