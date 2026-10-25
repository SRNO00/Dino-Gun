using UnityEngine;

public class BossAI : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bossBulletPrefab; // Prefab กระสุนของบอส
    public float fireRate = 2f;         // ยิงทุกๆ กี่วินาที
    public float bulletSpeed = 6f;      // ความเร็วกระสุน
    private float nextFireTime = 0f;

    [Header("Spread Shot Settings")]
    public int bulletCount = 3;         // จำนวนกระสุนที่ยิงออกไปพร้อมกัน (เช่น 3 หรือ 5 นัด)
    public float spreadAngle = 15f;     // มุมกระจายระหว่างนัด (องศา)

    private Transform playerTransform;

    void Update()
    {
        // ค้นหาตำแหน่งผู้เล่นตลอดเวลา
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
            return;
        }

        // เช็คเวลายิงกระสุน
        if (Time.time >= nextFireTime)
        {
            ShootSpread();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootSpread()
    {
        if (bossBulletPrefab == null || playerTransform == null) return;

        // คำนวณทิศทางหลักพุ่งไปหาผู้เล่น
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // คำนวณมุมเริ่มต้นสำหรับการยิงกระจาย
        float startAngle = baseAngle - (spreadAngle * (bulletCount - 1) / 2f);

        for (int i = 0; i < bulletCount; i++)
        {
            // คำนวณมุมของกระสุนแต่ละนัด
            float currentAngle = startAngle + (spreadAngle * i);

            // แปลงมุมเป็นทิศทาง (Vector2)
            Vector2 bulletDir = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

            // จุดเกิดกระสุนขยับออกห่างจากตัวบอสเล็กน้อยเพื่อไม่ให้ชนตัวเอง
            Vector2 spawnPos = (Vector2)transform.position + (bulletDir * 1.0f);

            // หมุนหัวกระสุนให้หันไปตามทิศทางที่ยิง
            Quaternion rotation = Quaternion.Euler(0, 0, currentAngle - 90f);

            // สร้างกระสุน
            GameObject BulletBoss = Instantiate(bossBulletPrefab, spawnPos, rotation);

            // ตั้งค่าความเร็ว
            Rigidbody2D rb = BulletBoss.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = bulletDir * bulletSpeed;
            }
        }
    }
}