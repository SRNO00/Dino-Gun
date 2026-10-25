using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // เมื่อกดปุ่ม Play ไปที่ฉากเกม
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // เมื่อกดปุ่มเลือกด่าน ให้ไปที่ฉาก LevelSelectScene
    public void OpenSelectLevel()
    {
        SceneManager.LoadScene("LevelSelectScene"); // ตรวจสอบชื่อฉากให้ตรงกับที่สร้างไว้
    }

    // เมื่อกดปุ่ม Quit ออกจากเกม
    public void QuitGame()
    {
        Debug.Log("Exiting Game...");

        // สำหรับออกเกมเมื่อ Build เป็นไฟล์จริง (.exe)
        Application.Quit();

        // สำหรับหยุดการเล่นเกมทันทีเมื่อทดสอบอยู่ในโปรแกรม Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}