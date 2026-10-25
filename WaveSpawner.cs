using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner Instance;

    [Header("Enemy Prefabs")]
    public GameObject[] enemyPrefabs;

    [Header("Spawn Settings")]
    public int maxEnemiesToSpawn = 10;
    private int spawnedCount = 0;
    private int defeatedCount = 0;
    public float spawnInterval = 2f; // ระยะเวลาในการเสกแต่ละตัว (วินาที)
    private float spawnTimer = 0f;

    [Header("UI References")]
    public TextMeshProUGUI killCountText;
    public GameObject transitionPanel;
    public TextMeshProUGUI transitionText;

    [Header("Next Level Settings")]
    public string nextSceneName = "Level2";
    private bool levelCleared = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (transitionPanel != null)
        {
            transitionPanel.SetActive(false);
        }
        UpdateUI();
    }

    void Update()
    {
        // ใช้ระบบจับเวลาใน Update เพื่อความเสถียร ป้องกันลูปหลุด
        if (spawnedCount < maxEnemiesToSpawn && !levelCleared)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0f; // รีเซ็ตเวลา
                SpawnEnemy();
                spawnedCount++;
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedPrefab = enemyPrefabs[randomIndex];

        if (selectedPrefab != null)
        {
            // สุ่มตำแหน่งเกิดรอบๆ จุด Spawner
            Vector2 randomPos = (Vector2)transform.position + (Random.insideUnitCircle.normalized * Random.Range(3f, 6f));
            Instantiate(selectedPrefab, randomPos, Quaternion.identity);
        }
    }

    // ฟังก์ชันรับค่าเมื่อศัตรูตาย
    public void AddKill()
    {
        defeatedCount++;
        UpdateUI();

        // เมื่อกำจัดครบตามจำนวนที่กำหนด
        if (defeatedCount >= maxEnemiesToSpawn && !levelCleared)
        {
            levelCleared = true;
            StartCoroutine(ProceedToNextLevelRoutine());
        }
    }

    void UpdateUI()
    {
        if (killCountText != null)
        {
            killCountText.text = "Enemies: " + defeatedCount + " / " + maxEnemiesToSpawn;
        }
    }

    IEnumerator ProceedToNextLevelRoutine()
    {
        if (transitionPanel != null)
        {
            transitionPanel.SetActive(true);
        }
        if (transitionText != null)
        {
            transitionText.text = "Stage Cleared! Loading " + nextSceneName + "...";
        }
        // ตรวจสอบว่าถ้าฉากถัดไปคือ WinScene ให้เปลี่ยนข้อความตามต้องการ
        if (nextSceneName == "WinScene")
        {
            transitionText.text = "Stage Cleared! Victory!";
        }
        else
        {
            transitionText.text = "Stage Cleared! Loading " + nextSceneName + "...";
        }

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nextSceneName);
    }
}