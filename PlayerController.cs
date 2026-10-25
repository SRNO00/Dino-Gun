using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 mousePos;

    [Header("Shooting")]
    public GameObject bulletPrefab;

    [Header("Audio Settings")]
    public AudioClip shootSound;       // ไฟล์เสียงปืน (.mp3 / .wav)
    private AudioSource audioSource;   // ตัวเล่นเสียง

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>(); // ดึงคอมโพเนนต์ Audio Source อัตโนมัติ
    }

    void Update()
    {
        // 1. รับค่าการเดิน WASD
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;

        // 2. รับตำแหน่งเมาส์
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 3. ยิงกระสุนเมื่อคลิกซ้าย
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        // เคลื่อนที่
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);

        // หันหน้าตามเมาส์
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void Shoot()
    {
        // สร้างกระสุน
        Instantiate(bulletPrefab, transform.position, transform.rotation);

        // เล่นเสียงปืนทุกครั้งที่ยิง (แก้ไขตรงนี้ให้เหลือแค่นี้ครับ)
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}