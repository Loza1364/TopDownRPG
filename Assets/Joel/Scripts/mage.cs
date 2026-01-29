using UnityEngine;

public class mage : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer sr;

    [SerializeField] private float speed = 5f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // point at cursor
        Vector2 direction = Camera.main.ScreenToWorldPoint(player.transform.position) - player.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 180f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 15 * Time.deltaTime);
        Vector2 lookDir = Camera.main.ScreenToWorldPoint(player.transform.position);
        Vector3 mouse = lookDir;
        transform.position += (mouse - player.transform.position).normalized * speed;
        sr.flipX = angle > 90 || angle < -90;
    }
}
