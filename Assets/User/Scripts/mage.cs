using UnityEngine;
using UnityEngine.UI;

public class mage : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject enemyBullet;
    public Slider enemyHP;
    private float HP = 100;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("Shoot", 1, 1.4f);
    }

    void Shoot()
    {
        Instantiate(enemyBullet, transform);
    }

    void Update()
    {
        // point at cursor
        Vector2 direction = player.transform.position - transform.position;
        if (direction.magnitude > 1)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            sr.flipX = angle < -90 || angle > 90;
            rb.linearVelocity = new Vector2(direction.normalized.x * speed, direction.normalized.y * speed);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
        if (player.transform.position.y < transform.position.y)
        {
            sr.sortingLayerName = "back";
        }
        else
        {
            sr.sortingLayerName = "front";
        }
        enemyHP.value = HP;
    }

    public void Hurt(int damage)
    {
        HP += damage;
        if (HP <= 0)
        {
            Destroy(this.gameObject);
            audioManager audio = GameObject.FindWithTag("audioManager").GetComponent<audioManager>();
            audio.PlaySFX(2);
        }
    }
}