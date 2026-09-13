using UnityEngine;

public class ParallaxMove : MonoBehaviour
{
    public float speed = 0.5f;
    public Transform other;   // الخلفية الثانية

    float width;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        width = sr.bounds.size.x;   // نحسب عرض الصورة مرة وحدة
    }

    void Update()
    {
        // تحريك لليسار
        transform.position += Vector3.left * speed * Time.deltaTime;

        // لو صرنا أبعد من الخلفية الثانية، نرجع يمينها
        if (transform.position.x <= other.position.x - width)
        {
            transform.position = new Vector3(
                other.position.x + width,
                transform.position.y,
                transform.position.z
            );
        }
    }
}