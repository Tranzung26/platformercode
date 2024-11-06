using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
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
