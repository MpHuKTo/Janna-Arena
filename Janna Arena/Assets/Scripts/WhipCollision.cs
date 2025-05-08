using UnityEngine;

public class WhipCollision : MonoBehaviour
{

    PlayerController controller;


    private void Awake()
    {
        controller = GetComponentInParent<PlayerController>();
    }

        private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Enemy"))
    {
        Enemy enemy =  collision.GetComponent<Enemy>();
        enemy.GetDamage(controller.Whip.Damage);
    }
    if (collision.CompareTag("RangeEnemy"))
    {
        RangeEnemy Rangeenemy = collision.GetComponent<RangeEnemy>(); 
        Rangeenemy.GetDamage(controller.Whip.Damage);
    }
}

}
