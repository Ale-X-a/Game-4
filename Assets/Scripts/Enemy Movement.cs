using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField]  float speed = 2f;
    Rigidbody2D rb2d;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
        rb2d.linearVelocity = new Vector2(speed, rb2d.linearVelocity.y); 
    }

    void OnTriggerExit2D(Collider2D other)
    {
        speed = -speed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rb2d.linearVelocity.x)), transform.localScale.y);
    }
    

}
