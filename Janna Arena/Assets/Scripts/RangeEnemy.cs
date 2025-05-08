using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    public Transform player;
    public float speed = 2f; 
    public float shootingRange = 5f;
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float nextFireTime = 2f; // Время следующего выстрела
    [SerializeField] private float health;
    [SerializeField] private GameObject textPrefab;

    private float currentHealth;
    private Transform enemyTransform;
    private Vector3 offsetOfDamageNumber = new Vector3(0, 2, 0); 
    

    private PlayerController playerController;



    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > shootingRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                if (Time.time >= nextFireTime)
                {
                    Shoot();
                    nextFireTime = Time.time + 1f / fireRate;
                }
            }
        }
    }
    protected void Awake()
    {
        currentHealth = health;
        enemyTransform = transform;
        playerController = FindObjectOfType<PlayerController>();
    }


    void Shoot()
    {
        if (bulletPrefab != null)

        {
            // Создаем пулю и направляем ее в сторону игрока
            
            Vector2 direction = (player.position - transform.position).normalized;

            

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);


            GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
            bullet.GetComponent<Bullet>().SetDirection(direction);
            
        }
    }
     public void GetDamage(float damage)
    {
        currentHealth -= damage;

        GameObject textObject = Instantiate(textPrefab, enemyTransform.position + offsetOfDamageNumber, Quaternion.identity);

        
        TextMesh textMesh = textObject.GetComponent<TextMesh>();
        if (textMesh != null )
        {
            textMesh.text = damage.ToString();
             textMesh.fontSize = 8;
        }

        Destroy(textObject, 1f);

        if (currentHealth <= 0)
        {
            //Debug.Log("DIE");
            playerController.killedEnemies += 1;

            Destroy(gameObject);

            Experience_Level_Controller.instance.SpawnExp2(transform.position);
        }

        
    }
}