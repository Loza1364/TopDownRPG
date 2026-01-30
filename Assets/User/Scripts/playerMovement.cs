using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    // walk & sprint
    [Header("Player Data")]
    public float walkSpeed;
    public float runSpeed;

    // accessors
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 moveInput;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void Movement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void Update()
    {
        // player movement & flip sprite
        rb.linearVelocity =  new Vector2(moveInput.x * walkSpeed, moveInput.y * walkSpeed);
        if (moveInput.x != 0)
        {
            sr.flipX = moveInput.x < 0;
        }

        animator.SetBool("walk", moveInput != Vector2.zero);
    }
}
