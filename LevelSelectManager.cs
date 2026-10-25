using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    // ฟังก์ชันสำหรับเลือกด่าน โดยใส่ชื่อฉากเข้าไปใน Inspector
    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ปุ่มกลับไปหน้า Main Menu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // ชื่อฉาก Main Menu ของคุณ
    }
}