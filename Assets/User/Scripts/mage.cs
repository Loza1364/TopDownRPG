using UnityEngine;

public class mage : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [SerializeField] private float speed = 5f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // point at cursor
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sr.flipX = angle < -90 || angle > 90;
        rb.linearVelocity = new Vector2(direction.normalized.x * speed, direction.normalized.y * speed);
    }
}