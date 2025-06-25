using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;  // Needed for IEnumerator

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(0f, 0f);
   
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private AudioClip hitSound;
    private AudioSource audioSource;

    Vector2 moveInput;
    Rigidbody2D rb2d;
    Animator animator;
    CapsuleCollider2D capsuleCollider2d;
    BoxCollider2D boxCollider2D;

    bool isAlive = true;
    public bool IsAlive => isAlive;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider2d = GetComponent<CapsuleCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (!isAlive) return;

        Run();
        FlipSprite();
        Death();

        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.linearVelocity.x) > Mathf.Epsilon; // horizontal speed > 0
        animator.SetBool("isRunning", playerHasHorizontalSpeed);

    }

    void OnMove(InputValue value)
    {
        if (!isAlive) return;
        moveInput = value.Get<Vector2>();
        Debug.Log($"Move Input: {moveInput}");
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) return;
        if (!boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground", "Bouncy"))) return;
        if (value.isPressed)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * speed, rb2d.linearVelocity.y);
        rb2d.linearVelocity = playerVelocity;
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.linearVelocity.x) > Mathf.Epsilon;
        if (!playerHasHorizontalSpeed) return;

        transform.localScale = new Vector2(Mathf.Sign(rb2d.linearVelocity.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }

    public void Death()
    {
            if (capsuleCollider2d.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
            {
                isAlive = false;
                animator.SetTrigger("dead");
                rb2d.linearVelocity = deathKick;
                FindFirstObjectByType<GameSession>().ProcessPlayerDeath();
            }
    }
    
    public void ApplyKnockback(Vector2 knockbackForce)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Debug.Log("Applying Knockback");
            rb.AddForce(knockbackForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log("No Knockback");
        }

        if (hitEffect != null)
        {
            hitEffect.Play(); 
        }
        if (hitSound != null && audioSource != null)
        {
            Debug.Log("Playing sound");
            audioSource.PlayOneShot(hitSound);
            Debug.Log("Is audio playing? " + audioSource.isPlaying);
        }

    }


}

