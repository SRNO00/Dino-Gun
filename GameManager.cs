using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    // ฟังก์ชันสำหรับกดปุ่ม Restart
    public void RestartGame()
    {
        Time.timeScale = 1f; // คืนค่าเวลาให้ปกติ
        // โหลด Scene ปัจจุบันขึ้นมาใหม่
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ฟังก์ชันสำหรับกลับไปหน้า Main Menu (เพิ่มเข้ามาตรงนี้)
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // คืนค่าเวลาให้ปกติ
        SceneManager.LoadScene("MainMenu"); // ชื่อฉาก Main Menu ของคุณ
    }
}