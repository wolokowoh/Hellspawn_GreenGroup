using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator anim;
    GameObject player;
    float playerHealth;
    PlayerController playerController;
    EnemyHealth enemyHeatlh;
    bool playerInRange;
    float timer;
    int AttackDamage = 5;
    float timeBetweenAttacks = 0.5f;
    public float speed;
    public bool movingRight = true;

     void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //playerHealth = GameObject.Find("Player").GetComponent<TestGameManager>().hp;
        enemyHeatlh = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
        if (other.gameObject.CompareTag("Turn"))
        {
            if (movingRight)
            {
                movingRight = false;
            }
            else
            {
                movingRight = true;
            }
        }
    }

     void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRange && enemyHeatlh.currentHealth > 0)
        {
            Attack();
        }
        if (playerHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
        
        if (movingRight)
        {
            transform.Translate(0, 0, 2 * Time.deltaTime * speed);
            transform.localScale = new Vector3(20, 20, 20);
        }
        else
        {
            transform.Translate(0, 0, -2 * Time.deltaTime * speed);
            transform.localScale = new Vector3(-20, 20, -20);
        }
    }

    void Attack()
    {
        timer = 0f;
        if (playerHealth > 0)
        {
            playerController.TakeDamage(AttackDamage);
        }
    }
}
