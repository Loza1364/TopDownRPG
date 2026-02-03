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
    private float invunderableTime = 0;

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

        if (invunderableTime > 0)
        {
            invunderableTime -= Time.deltaTime;
            sr.color = new Color(1f, 1f, 1f, 0.5f - (0.5f * Mathf.Sin(20f * (invunderableTime/1.5f))));
        }
        else
        {
            sr.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void Hurt(int damage)
    {
        if(invunderableTime <= 0)
        {
            invunderableTime = 1.5f;
            audioManager audio = GameObject.FindWithTag("audioManager").GetComponent<audioManager>();
            audio.PlaySFX(1);

        }
    }
}
