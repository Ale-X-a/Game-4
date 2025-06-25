using UnityEngine;

public class RabbitChase: MonoBehaviour
{
    [SerializeField] float speed = 5f;
    GameObject player;
    PlayerMovement playerMovement;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    void Update()
    {
        if (player != null && playerMovement != null && playerMovement.IsAlive)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null && player.IsAlive)
            {
                player.Death(); 
                Debug.Log("Player killed by rabbit");
            }
        }
    }
}