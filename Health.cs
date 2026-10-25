using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI Reference")]
    public Slider healthSlider; // ลาก Slider มาใส่ตรงนี้

    void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " โดนความเสียหาย! เลือดเหลือ: " + currentHealth);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (CompareTag("Player"))
        {
            // เรียกใช้ GameManager สั่ง GameOver
            if (GameManager.instance != null)
            {
                GameManager.instance.GameOver();
            }
            else
            {
                // สำรองไว้กรณีไม่ได้ตั้ง Singleton ใน GameManager
                GameManager gm = FindFirstObjectByType<GameManager>();
                if (gm != null)
                {
                    gm.GameOver();
                }
            }

            gameObject.SetActive(false); // ปิดตัว Player เมื่อตาย
        }
        else
        {
            // === เพิ่มตรงนี้ครับ ===
            // แจ้งไปยัง WaveSpawner ว่าศัตรูตัวนี้ตายแล้ว
            if (WaveSpawner.Instance != null)
            {
                WaveSpawner.Instance.AddKill();
            }
            // ======================

            Destroy(gameObject); // ถ้าเป็น Enemy ให้ทำลายทิ้ง
        }
    }
}