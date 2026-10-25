using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;
    public int damage = 10;

    [Header("Target Settings")]
    public string targetTag = "Enemy"; // ใส่ "Enemy" สำหรับกระสุนผู้เล่น, ใส่ "Player" สำหรับกระสุนบอส

    public GameObject damagePopupPrefab; // ลาก DamageText Prefab มาใส่ช่องนี้ (ถ้ามี)

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // เช็คว่าชนเป้าหมายตามที่กำหนดไว้ใน targetTag หรือไม่
        if (hitInfo.CompareTag(targetTag))
        {
            Health targetHealth = hitInfo.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);

                // สร้างตัวเลขดาเมจลอยขึ้นมาตรงจุดที่ยิงโดน
                if (damagePopupPrefab != null)
                {
                    GameObject popup = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity);
                    DamagePopup popupScript = popup.GetComponent<DamagePopup>();
                    if (popupScript != null)
                    {
                        popupScript.Setup(damage);
                    }
                }
            }

            // ทำลายกระสุนทิ้งเมื่อชนเป้าหมาย
            Destroy(gameObject);
        }
    }
}