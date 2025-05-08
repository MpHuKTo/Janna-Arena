using UnityEngine;

public class StaffProjectile : MonoBehaviour
{
    private float damage;
    private float speed;
    private float lifetime;
    private PlayerController playerController;
 

    private Vector3 direction;

 

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        speed = playerController.StaffProjSpeed;
       

    }


    public void Initialize(Transform enemyTarget, float projectileDamage, float projectileLifetime)
    {
        damage = projectileDamage;
        lifetime = projectileLifetime;

        if (enemyTarget != null)
        {
            
            direction = (enemyTarget.position - transform.position).normalized;
        }
        else
        {
           
            direction = Vector3.right; 
        }

        
        if (direction == Vector3.zero)
        {
            direction = Vector3.right; 
        }

        Destroy(gameObject, lifetime); 
    }

    void Update()
    {
        
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null && damage != 0)
            {
                enemy.GetDamage(damage);
               
            }
            Destroy(gameObject); 
        }
        if (collision.CompareTag("RangeEnemy"))
        {
            RangeEnemy Rangeenemy = collision.GetComponent<RangeEnemy>(); 
            Rangeenemy.GetDamage(damage);
            if (Rangeenemy != null && damage != 0)
            {
                Rangeenemy.GetDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
