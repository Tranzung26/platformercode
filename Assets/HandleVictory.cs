using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleVictory : MonoBehaviour
{
	// Tham chiếu tới Canvas
	public GameObject victoryCanvas;
	// Start is called before the first frame update
	void Start()
    {
		victoryCanvas.SetActive(false);
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
	    if (collision.CompareTag("Player"))
	    {
			Debug.Log("aaaaaaaaaa");
		    victoryCanvas.SetActive(true);
	    }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("DemoNextLevel");
    }

    public void PauseGame()
    {
        Time.timeScale = 0; // countinue = 1
    }
}
