using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Weapon
{
    Claws,
    Ice,
    Blood,
    Poison
}
public class PlayerController : MonoBehaviour
{
    public int bloodBarCurrentMP;
    public int bloodBarMaxMP;
    public int iceBarCurrentMP;
    public int iceBarMaxMP;
    public int poisonBarCurrentMP;
    public int poisonBarMaxMP;

    private Rigidbody playerRb;
    public int health;
    private int maxHealth;
    public float jumpForce;
    public float gravityMod;
    private float attackDamage = 5;
    public bool isOnGround = true;
    public bool gameOver = false;
    private Animator playerAnim;
    public ParticleSystem attackParticle;
    public ParticleSystem runningParticle;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    private AudioSource playerAudio;
    private bool isAttacking = false;
    public float speed;
    public float attackDelay = 1.0f;
    private TestGameManager TGManager;
    private bool death;
    public Weapon currentWeapon = Weapon.Claws;

    private void Awake()
    {
        // reset to default to fix our glitch
        // physics changes retain through scene transitions in Unity
        // so our gravity was being modded twice and thats 400 x default instead of 20
        // this is default gravity, reset it in Awake so start mods it right
        var gravity = new Vector3(0f, -9.81f, 0f);
        Physics.gravity = gravity;
    }
    void Start()
    {
        death = false;
        TGManager = GameObject.FindGameObjectWithTag("TGManager").GetComponent<TestGameManager>();
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityMod;
        playerAudio = GetComponent<AudioSource>();
        health = TGManager.hp;
    }

    void Update()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        if (!death)
        {
            if (health <= 0)
            {
                death = true;
                playerAnim.Play("Death");
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerAnim.Play("Jump");
            
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround && !gameOver)
        {
            playerAnim.Play("Jump");
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
        if (Input.GetKeyDown(KeyCode.W) && isOnGround && !gameOver)
        {
            playerAnim.Play("Jump");
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, -90, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, -90, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        if (Input.GetKey(KeyCode.G))
        {
            if (isAttacking == false)
            {
                isAttacking = true;
                int rand = Random.Range(1, 3);
                if (rand == 1)
                {
                    playerAnim.Play("LAttack");
                }
                else if (rand == 2)
                {
                    playerAnim.Play("LAttackMirror");
                }
                StartCoroutine("AttackTimer");
            }
        }

        if (isOnGround == true)
        {
            float move = Input.GetAxis("Horizontal");
            if (move < 0)
            {
                move = move * -1;
            }

            playerAnim.SetFloat("InputX", move);
        }
    }


    void FixedUpdate()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f);
        playerRb.AddForce(movement * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    private IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
        yield return null;
    }

    public void TakeDamage (int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            if (!TGManager.gameOver)
            {
                TGManager.UIGameOverTrigger = true;
            }
            
        }
    }
    // allows you to set the max in the game manager
    public void SetMaxHealth(int maxHP) => maxHealth = maxHP;
    // you can use this to load current HP from Save later OR INSTANT DEATH.
    public void SetCurrentHealth(int currentHP) => health = currentHP;

    public bool Heal()
    {
        bool used = health == maxHealth ? false : true;
        if (used)
        {
            int healAmount = 40;
            int v = health + healAmount;
            if (v >= maxHealth)
            {
                health = maxHealth;
            }
            else
            {
                health += healAmount;
            }
            return true;
        }
        return false;
        
    }
    public bool RestoreWeaponPower()
    {
        int restoreAmount = 20;
        if(currentWeapon == Weapon.Claws)
        {
            return false;
        }
        else if(currentWeapon == Weapon.Blood)
        {
            if(bloodBarCurrentMP == bloodBarMaxMP)
            {
                return false;
            }
            else if(bloodBarCurrentMP + restoreAmount >= bloodBarMaxMP)
            {
                bloodBarCurrentMP = bloodBarMaxMP;
            }
            else
            {
                bloodBarCurrentMP += restoreAmount;
            }
        }
        else if (currentWeapon == Weapon.Poison)
        {
            if(poisonBarCurrentMP == poisonBarMaxMP)
            {
                return false;
            }
            else if (poisonBarCurrentMP + restoreAmount >= poisonBarMaxMP)
            {
                poisonBarCurrentMP = poisonBarMaxMP;
            }
            else
            {
                poisonBarCurrentMP += restoreAmount;
            }
        }
        else // ice
        {
            if(iceBarCurrentMP == iceBarMaxMP)
            {
                return false;
            }
            else if (iceBarCurrentMP + restoreAmount >= iceBarMaxMP)
            {
                iceBarCurrentMP = iceBarMaxMP;
            }
            else
            {
                iceBarCurrentMP += restoreAmount;
            }
        }
        return true;
    }
    public void SetMaxPoison(int maxpoison)
    {
        poisonBarMaxMP = maxpoison;
    }
    public void SetMaxIce(int maxice)
    {
        iceBarMaxMP = maxice;
    }
    public void SetMaxBlood(int maxblood)
    {
        bloodBarMaxMP = maxblood;
    }
}

