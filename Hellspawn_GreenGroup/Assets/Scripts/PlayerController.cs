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
    public GameObject bloodAttack;
    public GameObject iceAttack;
    public GameObject poisonAttack;
    public int bloodBarCurrentMP;
    public int bloodBarMaxMP;
    public int bloodCastCost;
    public int iceBarCurrentMP;
    public int iceBarMaxMP;
    public int iceCastCost;
    public int poisonBarCurrentMP;
    public int poisonBarMaxMP;
    public int poisonCastCost;

    private Rigidbody playerRb;
    GameObject enemy;
    private EnemyHealth enemyHealth;
    public bool enemyInRange;
    public int health;
    private int maxHealth;
    public float jumpForce;
    public float gravityMod;
    public bool isOnGround = true;
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

    bool facingRight = true;

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
        health = TGManager.maxHP;
        iceBarCurrentMP = iceBarMaxMP;
        poisonBarCurrentMP = poisonBarMaxMP;
        bloodBarCurrentMP = bloodBarMaxMP;
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
        if (!death)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) 
                || Input.GetKeyDown(KeyCode.W))
                && isOnGround)
            {
                playerAnim.Play("Jump");
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
                transform.eulerAngles = new Vector3(0, -90, 0);
                facingRight = false;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
                transform.eulerAngles = new Vector3(0, 90, 0);
                facingRight = true;
            }
            if (Input.GetKey(KeyCode.G))
            {
                if (isAttacking == false)
                {
                    if (currentWeapon == Weapon.Claws)
                    {
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
                        if (enemyInRange && health > 0 && enemy)
                        {
                            Attack();
                        }
                    }
                    else if ((currentWeapon == Weapon.Ice))
                    {
                        if(iceBarCurrentMP > 0)
                        {
                            if (iceCastCost <= iceBarCurrentMP)
                            {
                                iceBarCurrentMP -= iceCastCost;
                                StartCoroutine("AttackTimer");
                                Vector3 position = transform.position;
                                if (facingRight)
                                {
                                    position.x += 5;
                                }
                                else
                                {
                                    position.x -= 2;
                                }
                                GameObject iceShard = Instantiate(iceAttack, position,
                                    iceAttack.transform.rotation);
                                Rigidbody attack = iceShard.transform.GetChild(0).GetComponent<Rigidbody>();
                                float forceX = 120f;

                                attack.AddForce(transform.forward * forceX);
                            }
                        }
                        
                        
                    }
                    else if ((currentWeapon == Weapon.Blood))
                    {
                        if( bloodBarCurrentMP > 0)
                        {
                            if (bloodCastCost <= bloodBarCurrentMP)
                            {
                                bloodBarCurrentMP -= bloodCastCost;
                                StartCoroutine("AttackTimer");
                                Vector3 position = transform.position;
                                if (facingRight)
                                {
                                    position.x += 5;
                                }
                                else
                                {
                                    position.x -= 2;
                                }
                                GameObject bloodShard = Instantiate(bloodAttack, position,
                                    bloodAttack.transform.rotation);
                                Rigidbody attack = bloodShard.transform.GetChild(0).GetComponent<Rigidbody>();
                                float forceX = 120f;

                                attack.AddForce(transform.forward * forceX);
                            }
                        }
                        
                        
                    }
                    else // POISON
                    {
                        if (poisonBarCurrentMP > 0)
                        {
                            if (poisonCastCost <= poisonBarCurrentMP)
                            {
                                poisonBarCurrentMP -= poisonCastCost;
                                StartCoroutine("AttackTimer");
                                Vector3 position = transform.position;
                                if (facingRight)
                                {
                                    position.x += 5;
                                }
                                else
                                {
                                    position.x -= 2;
                                }
                                GameObject poisonShard = Instantiate(poisonAttack, position,
                                    poisonAttack.transform.rotation);
                                Rigidbody attack = poisonShard.transform.GetChild(0).GetComponent<Rigidbody>();
                                float forceX = 120f;

                                attack.AddForce(transform.forward * forceX);
                            }
                        }
                        
                    }
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
        
    }

    void FixedUpdate()
    {
        if (!death)
        {


            playerRb = GetComponent<Rigidbody>();
            playerAnim = GetComponent<Animator>();
            playerAudio = GetComponent<AudioSource>();

            float moveHorizontal = Input.GetAxis("Horizontal");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f);
            playerRb.AddForce(movement * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemy = collision.gameObject;
            enemyInRange = true;
            
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyInRange = false;
        }
    }

    private IEnumerator AttackTimer()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
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

    public bool Heal(int potionCountHealth)
    {
        bool used = health == maxHealth ? false : true;
        if (used)
        {
            if(potionCountHealth == 0)
            {
                return false;
            }
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
    public bool RestoreWeaponPower(int potionCountMagic)
    {
        int restoreAmount = 20;
        if (potionCountMagic == 0)
        {
            return false;
        }
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

    void Attack()
    {
        if (health > 0)
        {
            playerAnim.Play("Attack");
            enemyHealth.TakeDamage("C");
        }
    }
}

