using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private int currentMapLevel;
    public GameObject messageUI;

    public Button level2Button;
    public Button level3Button;

    void Start()
    {
        currentMapLevel = PlayerPrefs.GetInt("CurrentMapLevel");

        //Kiểm tra nếu currentMapLevel< 2 thì disabled nút Level 2
        if (currentMapLevel < 2)
        {
            level2Button.interactable = false;
        }
        if (currentMapLevel < 3)
        {
            level3Button.interactable = false; 
        }
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
        if (currentMapLevel > 2) messageUI.SetActive(true);
        if (currentMapLevel == 2) SceneManager.LoadScene("DemoMap3");
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
