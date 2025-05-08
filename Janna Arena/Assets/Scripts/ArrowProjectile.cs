using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    private float damage;
    private float speed;
    private float lifetime;
    private PlayerController playerController;
    PlayerController controller;

    private Vector2 Dir;
    private Rigidbody2D rb;
    private SpriteRenderer Sprite;


    void Start()
    {

        playerController = FindObjectOfType<PlayerController>();
        speed = playerController.BowProjSpeed;
        damage = playerController.Bow.Damage;
        lifetime = playerController.Bow.AttackTime;
        Sprite = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();
    }



    private void Awake()
    {
        controller = GetComponentInParent<PlayerController>();
    }

    
    void Update()
    {

        rb.velocity = Dir;
        ScaleArrow(rb.velocity.x);
    }

    void ScaleArrow(float x)
    {
        if (x > 0.1f) // ��������� ������
        {
            Sprite.flipX = false;
        }
        else if (x < -0.1f) // ��������� �����
        {
            Sprite.flipX = true;
        }
    }

    public void Initialize(Vector2 Direction)
    {
        Dir = Direction;
        
    }
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null && damage != 0)
            {
                enemy.GetDamage(damage);
                Destroy(gameObject);
            }
        }
        if (collision.CompareTag("RangeEnemy"))
        {
            RangeEnemy Rangeenemy = collision.GetComponent<RangeEnemy>(); 
            Rangeenemy.GetDamage(damage);
            if (Rangeenemy != null && damage != 0)
            {
                Rangeenemy.GetDamage(damage);
                Destroy(gameObject);
            }
        }
        
    }
}
