using UnityEngine;

public class Coin : MonoBehaviour
{
    public float moveSpeed = 5f;  // سرعة حركة العملة
    public float deadZone = -25;  // النقطة اللي بعدها نحذف العملة

    void Update()
    {
        // نحرك العملة لليسار
        transform.position = transform.position + (Vector3.left * moveSpeed * Time.deltaTime);

        // نحذفها لما تطلع برا الشاشة
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }

    // لما اللاعب يلمس العملة
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            Debug.Log("coin collected!");
            // نزيد عدد العملات من LogicManager
            FindObjectOfType<LogicManager>().AddCoins(1);
            Destroy(gameObject);
        }
    }
}