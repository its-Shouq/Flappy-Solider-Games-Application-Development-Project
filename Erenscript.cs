using UnityEngine;


public class Erenscript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicManager logic;
    public bool charIsAlive = true;

    void Start()
    {

       // gameObject.name = "Eren";
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && charIsAlive)
        {
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, flapStrength);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit: " + collision.collider.name + " Tag: " + collision.collider.tag);

        if (collision.collider.CompareTag("Obstacle"))
        {
            // لو الشيلد شغّال → نتجاهل الصدمة ونرجع
            if (logic != null && logic.IsShieldActive())
            {
                Debug.Log("Shield active → ignore hit");
                return;
            }

            // لو ما فيه شيلد → نموت عادي
            logic.gameOver();
            GetComponent<AudioSource>().Play();
            charIsAlive = false;
        }
    }
}


