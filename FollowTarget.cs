using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target; // ตัวละคร/มอนสเตอร์ที่จะให้ตาม
    public Vector3 offset = new Vector3(0, 0.8f, 0); // ระยะห่างเหนือหัว

    // เปลี่ยนจาก void Update() มาใช้ LateUpdate()
    void LateUpdate()
    {
        if (target != null)
        {
            // อัปเดตตำแหน่งตาม target หลังจากตัวละครเดินเสร็จแล้วในเฟรมนั้น
            transform.position = target.position + offset;
        }
    }
}