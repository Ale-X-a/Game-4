using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement: MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] private float jumpSpeed = 5f;


Vector2 moveInput;
Rigidbody2D rb2d;
Animator animator;
CapsuleCollider2D capsuleCollider2D;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.linearVelocity.x) > Mathf.Epsilon; // horizontal speed > 0
        animator.SetBool("isRunning", playerHasHorizontalSpeed);
        
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log($"Move Input: {moveInput}");
    }

    void OnJump(InputValue value)
    {
        Debug.Log($"Jump Input: {value}");
        if (!capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        if (value.isPressed)
        {
            rb2d.linearVelocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * speed, rb2d.linearVelocity.y);
        rb2d.linearVelocity = playerVelocity;
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.linearVelocity.x) > Mathf.Epsilon; // horizontal speed > 0
        if (!playerHasHorizontalSpeed) return;
        {
            transform.localScale = new Vector2(Mathf.Sign(rb2d.linearVelocity.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }
    
}
