using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    // walk & sprint
    [Header("Player Data")]
    public float walkSpeed;
    public float runSpeed;
    public int HP = 6;

    // accessors
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 moveInput;
    private Animator animator;
    public float invunderableTime = 0;
    public Slider playerHP;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerHP.maxValue = HP;
    }

    public void Movement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void Update()
    {
        if (invunderableTime > 0)
        {
            invunderableTime -= Time.deltaTime;
            sr.color = new Color(1f, 1f, 1f, 0.5f - (0.5f * Mathf.Sin(20f * (invunderableTime / 1.5f))));
            if (invunderableTime > 0.95f)
            {
                animator.SetBool("hurt", true);
                if (invunderableTime > 1.3f)
                {
                    sr.color = Color.red;
                    rb.linearDamping = 7f;
                }
                else
                {
                    rb.linearDamping = 0;
                }
                    return;
            }
            else
            {
                rb.linearDamping = 0;
                animator.SetBool("hurt", false);
            }
        }
        else
        {
            sr.color = new Color(1f, 1f, 1f, 1f);
            animator.SetBool("hurt", false);
        }
        // player movement & flip sprite
        rb.linearVelocity =  new Vector2(moveInput.x * walkSpeed, moveInput.y * walkSpeed);
        if (moveInput.x != 0)
        {
            sr.flipX = moveInput.x < 0;
        }

        animator.SetBool("walk", moveInput != Vector2.zero);
    }

    public void Hurt(int damage)
    {
        if(invunderableTime <= 0)
        {
            if (HP - 1 <= 0)
            {
                HP = 6;
                playerHP.value = HP;
                transform.position = Vector2.zero;
                sr.flipX = false;
                Animator crossfade = GameObject.FindWithTag("crossfade").GetComponent<Animator>();
                crossfade.SetTrigger("fade");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
            }
            HP -= 1;
            playerHP.value = HP;
            invunderableTime = 1.5f;
            audioManager audio = GameObject.FindWithTag("audioManager").GetComponent<audioManager>();
            audio.PlaySFX(1);
            float angle = Random.Range(0, 2 * Mathf.PI);
            rb.linearVelocity = new Vector2(13*Mathf.Cos(angle), 13*Mathf.Sin(angle));
            playerScore score = GetComponent<playerScore>();
            if (score.score < 10)
            {
                return;
            }
            score.AddScore(-10);
        }
    }
}