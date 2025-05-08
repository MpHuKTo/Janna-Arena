using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience_Pickup : MonoBehaviour
{
    public Transform player;
    public float attractDistance;
    public float speed;
    public int xpAmount;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < attractDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Experience_Level_Controller.instance.GetExp(xpAmount);

            Destroy(gameObject);
        }
    }
}