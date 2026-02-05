using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player.transform.position.x < transform.position.x)
        {
            transform.right = -transform.right;
        }
        transform.position += transform.right * 1f;
    }

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        if (Mathf.Abs(transform.position.x) > 30)
        {
            Destroy(gameObject);
        }
        if (Mathf.Abs(transform.position.y) > 30)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement player = collision.gameObject.GetComponent<playerMovement>();
            if (player.invunderableTime <= 0)
            {
                player.Hurt(-1);
                Destroy(this.gameObject);
            }
        }
    }
}