using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float health;
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
    public TestGameManager TGManager;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityMod;
        playerAudio = GetComponent<AudioSource>();
        health = 50;
    }

    void Update()
    {
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
}

