using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator anim;
    GameObject player;
    private float playerHealth;
    PlayerController playerController;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;
    public int AttackDamage = 5;
    public float timeBetweenAttacks = 0.1f;
    public float speed = 2.0f;
    public enum directions {Right, Left};
    public directions direction = directions.Right;
    bool delayIsActive;

    void Awake()
    {
        

        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();

        timer = timeBetweenAttacks;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Turn"))
        {
            SwitchDirection();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerHealth = playerController.health;
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks)
        {
            // Attack delay has passed.
            transform.Translate(0, 0, 1 * Time.deltaTime * speed);

            if (playerInRange && enemyHealth.currentHealth > 0)
            {
                if (!delayIsActive)
                {
                    Attack();
                }
            }
        }
    }

    private IEnumerator AttackDelay()
    {
        delayIsActive = true;
        yield return new WaitForSeconds(1f);
        delayIsActive = false;
        
    }

    void Attack()
    {
        // Attack the player.
        // Additional behavior: Face the direction of the player and play an attack animation.
        timer = 0f;

        if (playerHealth > 0)
        {
            if (direction == directions.Right)
            {
               if (transform.position.x > player.transform.position.x)
                {
                    SwitchDirection();
                }
            }
            else if (direction == directions.Left)
            {
                if (transform.position.x < player.transform.position.x)
                {
                    SwitchDirection();
                }
            }

            anim.Play("Attack");
            playerController.TakeDamage(AttackDamage);
            StartCoroutine(AttackDelay());
        }
    }

    void SwitchDirection()
    {
        if (direction == directions.Right)
        {
            direction = directions.Left;
        }
        else if (direction == directions.Left)
        {
            direction = directions.Right;
        }

        float curY = transform.eulerAngles.y;
        transform.eulerAngles = new Vector3(0, -curY, 0);
    }
}