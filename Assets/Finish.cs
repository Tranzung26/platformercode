using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();
        if (collision.CompareTag("Player"))
        {
            playerController.SavePlayerData();
            // go to next map
            SceneController.instance.NextLevel();
        }
    }
}
