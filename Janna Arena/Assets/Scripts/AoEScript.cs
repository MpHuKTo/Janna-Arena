using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEScript : MonoBehaviour
{
    private PlayerController controller;
    private bool ReadyToAttack = false;


    private void AoeAttackCheck()
    {

        if (controller.AoeWpn.HasWeapon && controller.AoeWpn.CurCooldown <= 0 && !ReadyToAttack)
        {
            controller.AoeWpn.CurCooldown = controller.AoeWpn.Cooldown;
            controller.AoeWpn.CurAttackTime = controller.AoeWpn.AttackTime;

            ReadyToAttack = true;

        }
        else if (!ReadyToAttack)
        {
            controller.AoeWpn.CurCooldown -= Time.deltaTime;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInParent<PlayerController>();

        
    }

    // Update is called once per frame
    void Update()
    {
        AoeAttackCheck();
    }




    private void OnTriggerStay2D(Collider2D collision)
    {
        if (ReadyToAttack) { 
        // Получаем все коллайдеры в радиусе атаки
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x);

            // Наносим урон каждому врагу
            foreach (Collider2D enemyCollider in hitEnemies)
            {
                if (enemyCollider.CompareTag("Enemy"))
                {
                    Enemy enemy = enemyCollider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.GetDamage(controller.AoeWpn.Damage);
                    }
                }
                else if (enemyCollider.CompareTag("RangeEnemy"))
                {
                    RangeEnemy rangeEnemy = enemyCollider.GetComponent<RangeEnemy>();
                    if (rangeEnemy != null)
                    {
                        rangeEnemy.GetDamage(controller.AoeWpn.Damage);
                    }
                }
            }
            ReadyToAttack = false;
        }
    }
}
