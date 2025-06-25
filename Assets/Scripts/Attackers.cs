using UnityEngine;

public class Attackers : MonoBehaviour
{
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float knockbackForce = 5f;
    
    private float lastAttackTime;
    private GameObject player;
    private PlayerMovement playerMovement; 
    private Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerMovement = player.GetComponent<PlayerMovement>(); 
    }
    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }
    void Attack()
    {
        Debug.Log("ATTACK!");
        GetComponent<Animator>().SetTrigger("Attack");

        if (playerMovement != null)
        {
            Vector2 knockbackDir = (player.transform.position - transform.position).normalized;
            Vector2 knockback = knockbackDir * knockbackForce;
            playerMovement.ApplyKnockback(knockback);
        }

        if (animator != null)
        {
            animator.SetTrigger("Attack"); 
        }
    }

}
