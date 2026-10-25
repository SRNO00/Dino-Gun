using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float moveSpeed = 1f;
    private float disappearTimer = 0.5f;
    private Color textColor;

    void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount)
    {
        textMesh.text = damageAmount.ToString();
        textColor = textMesh.color;
    }

    void Update()
    {
        // ลอยขึ้นบนเรื่อยๆ
        transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;

        // ค่อยๆ จางหายไป
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            textColor.a -= 3f * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}