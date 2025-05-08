using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.TextCore.Text;
using System;

public class Enemy : MonoBehaviour
{
    Animator anim;
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] public float damage;
    [SerializeField] Vector2 direction;
    [SerializeField] private GameObject textPrefab;

    private Vector3 offsetOfDamageNumber = new Vector3(0, 2, 0); 
    private float currentHealth;
    private GameObject characterPlayer;
    private Rigidbody2D Rigidbody2D;
    private Transform enemyTransform;
    
   private PlayerController playerController;

    


    protected void Awake()
    {
        currentHealth = health;
        characterPlayer = GameObject.FindGameObjectWithTag("Player");
        Rigidbody2D = GetComponent<Rigidbody2D>();
        enemyTransform = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerController>();
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

            Experience_Level_Controller.instance.SpawnExp1(transform.position);
        }

        
    }


    protected void Update()
    {
        if (direction.x > 0)
        {

            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected void Move()
    {
        Vector2 PlayerPos = characterPlayer.transform.position;

        direction = PlayerPos - (Vector2)transform.position;

        Rigidbody2D.MovePosition((Vector2)transform.position + new Vector2(Mathf.Clamp(direction.x, -1, 1), Mathf.Clamp(direction.y, -1, 1)) * speed * Time.deltaTime);
        if (anim != null)
        {
            anim.SetBool("run", true);
        }

    }


}
