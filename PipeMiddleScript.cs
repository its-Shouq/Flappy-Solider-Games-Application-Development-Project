using UnityEngine;

public class PipeMiddleScript : MonoBehaviour
{
    public LogicManager logic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    // if(collision.gameObject.layer == 3)
    // {
    // logic.addScore(1);
    // }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Middle trigger with: " + collision.name + "  Tag: " + collision.tag); // لفحص المرور
        if (collision.CompareTag("player"))
        {
            logic.addScore(1);
        }
    }

}
       
    

    

