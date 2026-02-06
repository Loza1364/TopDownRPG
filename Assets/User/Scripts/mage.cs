using UnityEditor.ShaderGraph;
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
    private float FlashTime = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("Shoot", Random.Range(0.5f,5f), 1.4f);
    }

    void Shoot()
    {
        Instantiate(enemyBullet, transform);
        audioManager audio = GameObject.FindWithTag("audioManager").GetComponent<audioManager>();
        audio.PlaySFX(4);
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
        if (FlashTime > 0)
        {
            FlashTime -= Time.deltaTime;
            sr.color = Color.red;
        }
        else
        {
            sr.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void Hurt(int damage)
    {
        HP += damage;
        FlashTime = 0.09f;
        screenEffects cam = GameObject.FindWithTag("MainCamera").GetComponent<screenEffects>();
        if (HP <= 0)
        {
            cam.start = true;
            playerScore score = GameObject.FindWithTag("Player").GetComponent<playerScore>();
            score.AddScore(500);
            audioManager audio = GameObject.FindWithTag("audioManager").GetComponent<audioManager>();
            audio.PlaySFX(1);
            spawnLoop spawn = GameObject.FindWithTag("spawner").GetComponent<spawnLoop>();
            spawn.enemyCount--;
            Destroy(this.gameObject);
        }
    }
}