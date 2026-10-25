using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 10;
    public float attackCooldown = 1.0f; // หน่วงเวลาการตี (วินาที)
    private float nextAttackTime = 0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // เช็กว่าถึงเวลาที่ตีได้อีกครั้งหรือยัง
            if (Time.time >= nextAttackTime)
            {
                Health playerHealth = other.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attackDamage);
                    nextAttackTime = Time.time + attackCooldown; // ตั้งเวลาคูลดาวน์ถัดไป
                }
            }
        }
    }

    // กรณีใช้ OnCollisionStay2D ให้เปลี่ยนมาใช้ก้อนนี้แทนครับ
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= nextAttackTime)
            {
                Health playerHealth = collision.gameObject.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attackDamage);
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
    }
}