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
    public bool playerInRange;
    public bool playerInAttackRange;
    public bool playerTouching;
    float timer;
    public int AttackDamage = 5;
    public float attackAnimationLength = 0.5f;
    public float howLongToWaitBeforeAttackAnimDoesDamage = 1f;
    public float speed = 2.0f;
    public enum directions {Right, Left};
    public directions direction = directions.Right;
    bool delayIsActive;

    void Awake()
    {
        

        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();

        timer = attackAnimationLength;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Turn"))
        {
            SwitchDirection();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTouching = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTouching = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerHealth = playerController.health;
        timer += Time.deltaTime;
        if (playerInRange)
        {
            TurnToPlayer();
        }
        if (timer >= attackAnimationLength)
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
    private void TurnToPlayer()
    {
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
        }
    }
    private IEnumerator AttackDelay()
    {
        delayIsActive = true;
        float delay = 1f;
        delay += attackAnimationLength;
        yield return new WaitForSeconds(delay);
        delayIsActive = false;
        
    }

    void Attack()
    {
        // Attack the player.
        // Additional behavior: Face the direction of the player and play an attack animation.
        timer = 0f;

        if (playerHealth > 0)
        {
            anim.Play("Attack");
            StartCoroutine(DealDamage());
            StartCoroutine(AttackDelay());
        }
        
        
    }
    IEnumerator DealDamage()
    {
        bool damageAlreadyDone = false;
        float animLength = attackAnimationLength - howLongToWaitBeforeAttackAnimDoesDamage;
        yield return new WaitForSeconds(howLongToWaitBeforeAttackAnimDoesDamage);
        float timeToWait = animLength / (0.05f);
        for (float i = 0; i < timeToWait; i+=.05f)
        {
            yield return new WaitForSeconds(.05f);
            if (playerTouching && !damageAlreadyDone)
            {
                playerController.TakeDamage(AttackDamage);
                damageAlreadyDone = true;

            }
            else if(damageAlreadyDone)
            {
                break;
            }
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