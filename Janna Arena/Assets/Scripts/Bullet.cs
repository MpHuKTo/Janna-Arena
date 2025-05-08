using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 direction;
    public float damage;
    [SerializeField] float DeathTime;

    private SpriteRenderer Sprite;
    private Rigidbody2D rb2;


    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }


    void Start()
    {
        Destroy(gameObject, DeathTime);
        Sprite = GetComponent<SpriteRenderer>();
        rb2 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Движение пули
        rb2.velocity = direction * speed;
        
    }

    

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}