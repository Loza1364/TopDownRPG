using UnityEngine;
using UnityEngine.InputSystem;

public class playerWeapon : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer sr;
    private audioManager audios;

    [Header("Direction")]
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private GameObject bullet;
    private Vector2 mousePosition;
    private float bulletTimer;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        audios = GameObject.FindWithTag("audioManager").GetComponent<audioManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void Aim(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed && bulletTimer <= 0)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            bulletTimer = 0.2f;
            audios.PlaySFX(3);
        }
    }

    void LateUpdate()
    {
        // point at cursor
        Vector2 direction = Camera.main.ScreenToWorldPoint(mousePosition) - player.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotateSpeed * Time.deltaTime);
        Vector2 lookDir = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 mouse = lookDir;
        // update position
        transform.position = player.transform.position + (mouse - player.transform.position).normalized * 0.5f;
        transform.position -= new Vector3(0, .3f, 0);
        // bullet cooldown
        if (bulletTimer > 0) { bulletTimer -= Time.deltaTime; }
        sr.flipY = angle > 90 || angle < -90;
    }
}