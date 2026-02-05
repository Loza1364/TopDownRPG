using UnityEngine;

public class playerBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    void Start()
    {
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
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<mage>().Hurt(-6);
            audioManager audios = GameObject.FindWithTag("audioManager").GetComponent<audioManager>();
            audios.PlaySFX(1);
            Destroy(this.gameObject);
        }
    }
}