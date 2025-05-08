using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using static UpgradesScript;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private float moveX;
    private float moveY;
    public Joystick joystick;

    public float health;
    public float pickupRange;
    public float hpRegen;

    private Vector2 lastfacedDirection;



    [SerializeField] protected float invincibilityTime;

    [SerializeField] GameObject WhipObj;


    [SerializeField] GameObject ProjectileStaff;

    [SerializeField] GameObject ArrowProj;

    Animator anim;



    public SpriteRenderer spriteRenderer;
    protected float invincibilityCurrent;
    public float initialHealth; // Для хранения максимального здоровья
    



    private SpriteRenderer spriteRenderForWhip;
    private Vector3 whipLeftCords;
    private Vector3 whipRightCords;

    // Событие для обновления HP
    public event Action<float, float> OnHealthChanged;

    public bool LastFacedRight;

    [System.Serializable]
    public struct Weapon
    {
        public bool HasWeapon;
        public float Cooldown;
        public float Damage;
        public float AttackTime;
        [HideInInspector]
        public float CurAttackTime;
        [HideInInspector]
        public float CurCooldown;

        public Sprite Icon;

        public int ProjectileAmmount; 
        
    }

    public Weapon Whip;
    public Weapon Staff;
    public Weapon Bow;
    public Weapon AoeWpn;
    [SerializeField] public GameObject AoEScript;


    public float StaffProjSpeed;
    public float BowProjSpeed;


    [SerializeField] public float regenTime;
    [SerializeField] public float curRegenTime;

    [SerializeField] public GameObject DieUi;

    public int killedEnemies = 0;
    

    private void Start()
    {
        
        initialHealth = health; // Сохраняем начальное здоровье
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        OnHealthChanged?.Invoke(health, initialHealth); // Уведомляем о начальном состоянии здоровья
        LastFacedRight = true;
        StaffProjSpeed = 3;
        BowProjSpeed = 4;
        curRegenTime = regenTime;
        spriteRenderForWhip = WhipObj.GetComponent<SpriteRenderer>();
        
        

        

    }



    private void FixedUpdate()
    {
        Move();
        WhipAttackCheck();
        StaffAttackCheck();
        BowAttackCheck();
        regenerateHealth();
       activateAoe();
    }


    private void regenerateHealth()
    {

        if (curRegenTime <= 0 && health + hpRegen < initialHealth && hpRegen != 0)
        {
            health += hpRegen;
            curRegenTime = regenTime;
        }
        else
        {
            if (curRegenTime >= 0)
            {
                curRegenTime -= Time.deltaTime;
                HPbar.instance.UpdateHealthBar(health, initialHealth);
            }
        }


    }

    
    private void WhipAttackCheck()
    {

        if (Whip.HasWeapon && Whip.CurCooldown <= 0)
        {
            Whip.CurCooldown = Whip.Cooldown;
            Whip.CurAttackTime = Whip.AttackTime;
            WhipAttack(); // Начало атаки
        }
        else if (Whip.CurAttackTime > 0)
        {
            WhipAttack(); // Продолжение атаки
        }
        else
        {
            Whip.CurCooldown -= Time.deltaTime;

        }


    }


    private void WhipAttack()
    {
        float whipOffset = 2f;

        whipLeftCords = new Vector3(transform.position.x - whipOffset, transform.position.y, - 1);
        whipRightCords = new Vector3(transform.position.x + whipOffset, transform.position.y, - 1);

        if (Whip.CurAttackTime > 0)
        {
            WhipObj.SetActive(true);
            Whip.CurAttackTime -= Time.deltaTime;

            if (rb.velocity.x > 0 || LastFacedRight) // Правая атака
            {
                
                spriteRenderForWhip.flipX = false;
                WhipObj.transform.position = whipRightCords;
            }
            else if (rb.velocity.x < -0.00001f || !LastFacedRight) // Левая атака
            {

                spriteRenderForWhip.flipX = true;
                WhipObj.transform.position = whipLeftCords;
            }
           

           if (Whip.CurAttackTime <= 0)
            {
                WhipObj.SetActive(false);
                
            }
        }
        else
        {
            WhipObj.SetActive(false);
        }
    }

    
    private void StaffAttackCheck()
    {
        
        if (Staff.HasWeapon && Staff.CurCooldown <= 0)
        {
            Staff.CurCooldown = Staff.Cooldown;
            Staff.CurAttackTime = Staff.AttackTime;

            StartCoroutine(LaunchStaffProjectiles());

        }
        else
        {
            Staff.CurCooldown -= Time.deltaTime;

        }


    }

    private IEnumerator LaunchStaffProjectiles()
    {
        for (int i = 0; i < Staff.ProjectileAmmount; i++)
        {
            StaffAttack();
 
            yield return new WaitForSeconds(1f);
        }
    }


    private Transform FindNearestEnemy()
    {
        float nearestDistance = float.MaxValue;
        Transform nearestEnemy = null;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy == null) continue;
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }

    private void StaffAttack() 
    {
        Transform nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {

            GameObject projectile = Instantiate(ProjectileStaff, transform.position, Quaternion.identity);
            if (!projectile.activeSelf)
            {
                projectile.SetActive(true); 
            }
            StaffProjectile projScript = projectile.AddComponent<StaffProjectile>();

            
            projScript.Initialize(nearestEnemy, Staff.Damage, Staff.AttackTime);

        }




    }


    private void BowAttackCheck()
    {

        if (Bow.HasWeapon && Bow.CurCooldown <= 0)
        {
            Bow.CurCooldown = Bow.Cooldown;
            Bow.CurAttackTime = Bow.AttackTime;

            StartCoroutine(LaunchBowProjectiles());

        }
        else
        {
            Bow.CurCooldown -= Time.deltaTime;

        }

    }

    private IEnumerator LaunchBowProjectiles()
    {
        for (int i = 0; i < Bow.ProjectileAmmount; i++)
        {
            BowAttack();

            yield return new WaitForSeconds(1f);
        }
    }

    private void BowAttack()
    {
        
        float angle = Mathf.Atan2(lastfacedDirection.y, lastfacedDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);


        GameObject Arrow = Instantiate(ArrowProj, transform.position, rotation) ;
        if (!Arrow.activeSelf)
        {       
            Arrow.SetActive(true);
        }
        ArrowProjectile projScript = Arrow.AddComponent<ArrowProjectile>();

        
       
        Vector2 DirForArrow = new Vector2 (lastfacedDirection.x * BowProjSpeed, lastfacedDirection.y * BowProjSpeed);
        


        if (DirForArrow.magnitude == 0) { DirForArrow = Vector2.right * BowProjSpeed; }
        projScript.Initialize(DirForArrow);
    }

    private void activateAoe()
    {
        if (AoeWpn.HasWeapon)
        {
            if (!AoEScript.activeSelf)
            {
                AoEScript.SetActive(true);
            }
        }
    }





    void Move()
    {
       moveX = joystick.Horizontal;
        moveY = joystick.Vertical;
        rb.velocity = new Vector2(moveX * speed, moveY * speed);

        if (rb.velocity != Vector2.zero){ //переключение между анимаций
            anim.SetBool("Move", true);
            lastfacedDirection =  new Vector2(moveX, moveY);
        }
        else{
            anim.SetBool("Move", false);
        }

        if (invincibilityCurrent >= 0)
        {
            invincibilityCurrent -= Time.deltaTime;
        }

        ScalePlayer(rb.velocity.x);
    }


    void ScalePlayer(float x)
{
    if (x > 0.1f) // Двигаемся вправо
    {
        LastFacedRight = true;
        spriteRenderer.flipX = false;
    }
    else if (x < -0.1f) // Двигаемся влево
    {
        LastFacedRight = false;
        spriteRenderer.flipX = true;
    }
}


    private void charGetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Debug.Log("Bro is dead");

            Time.timeScale = 0f;

            DieUi.SetActive(true);




        }
        

        OnHealthChanged?.Invoke(health, initialHealth); // Уведомляем об изменении здоровья
        PlayerCamera.instance.CameraShake();
    }


   

    protected void OnCollisionStay2D(Collision2D coll)
    {

        if (invincibilityCurrent <= 0)
        {
            if (coll.gameObject.TryGetComponent(out Enemy enemy))
            {
                charGetDamage(enemy.damage);
                invincibilityCurrent = invincibilityTime;
                StartCoroutine(Blinking());
            }
            if (coll.gameObject.tag == "Heal")
            {
                coll.gameObject.TryGetComponent(out Heal heal);
                if (health + heal.HealHP < initialHealth)
                {
                    health += heal.HealHP;
                    HPbar.instance.UpdateHealthBar(health, initialHealth);
                    heal.destroy();
                }
                

                

            }
        }
    }

    IEnumerator Blinking()
    {
        bool flag = true;
        while (true)
        {
            if (invincibilityCurrent > 0)
            {
                yield return new WaitForSeconds(invincibilityCurrent / 10);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, flag ? 0.2f : 1f);
                flag = !flag;
            }
            else
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
                break;
            }
        }
    }


    public void TakeDamage(float damage)
{
    if (invincibilityCurrent <= 0)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;


                

                

            
        }

        OnHealthChanged?.Invoke(health, initialHealth); // Уведомляем об изменении здоровья
        PlayerCamera.instance.CameraShake();
        invincibilityCurrent = invincibilityTime; // Включаем время неуязвимости
        StartCoroutine(Blinking());
    }
}
}
