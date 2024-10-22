using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Import để sử dụng SceneManager
using UnityEngine.UI; // Import để sử dụng UI

public class HandleTryAgain : MonoBehaviour
{
	public GameObject gameOverUI; // UI để hiển thị khi thua
	public Button tryAgainButton; // Nút Try Again
	public int playerHealth = 100; // Máu ban đầu của player
	private float damageInterval = 1f; // Thời gian giữa các lần trừ máu
	private bool isGameOver = false; // Trạng thái game over

	// Start is called before the first frame update
	void Start()
	{
		gameOverUI.SetActive(false); // Ẩn UI khi bắt đầu
		tryAgainButton.onClick.AddListener(TryAgain); // Thêm listener cho nút

		// Bắt đầu trừ máu mỗi giây
		InvokeRepeating("TakeDamageOverTime", damageInterval, damageInterval);
	}

	// Update is called once per frame
	void Update()
	{
		// Nếu máu <= 0 và chưa game over, kích hoạt GameOver
		if (playerHealth <= 0 && !isGameOver)
		{
			GameOver();
		}
	}

	// Hàm xử lý khi thua game
	void GameOver()
	{
		isGameOver = true; // Đánh dấu game đã over
		playerHealth = 0; // Đảm bảo máu không giảm xuống dưới 0
		gameOverUI.SetActive(true); // Hiển thị UI game over
		Time.timeScale = 0f; // Tạm dừng game
		CancelInvoke("TakeDamageOverTime"); // Dừng việc trừ máu thêm
		Debug.Log("Game Over"); // In ra console để kiểm tra
	}

	// Hàm được gọi khi nhấn nút Try Again
	void TryAgain()
	{
		Time.timeScale = 1f; // Khởi động lại thời gian
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload lại scene hiện tại
	}

	// Hàm trừ máu mỗi giây
	void TakeDamageOverTime()
	{
		if (!isGameOver) // Chỉ trừ máu nếu game chưa over
		{
			TakeDamage(20); // Trừ 20 máu
		}
	}

	// Hàm trừ máu
	public void TakeDamage(int damage)
	{
		if (!isGameOver) // Kiểm tra nếu game chưa over
		{
			playerHealth -= damage; // Trừ máu
			if (playerHealth < 0) playerHealth = 0; // Đảm bảo máu không giảm dưới 0
			Debug.Log("Player Health: " + playerHealth); // Hiển thị lượng máu còn lại trong Console
		}
	}
}
