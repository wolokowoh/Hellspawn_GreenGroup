using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    Animator anim;
    bool isDead;
    bool isSinking;
    Rigidbody rigidbody;
    public int BloodDamage;
    public int PoisonDamage, ClawsDamage;
    public int IceDamage;
    public float poisonLength;

    bool isPoisoned;

    void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Blood"))
        {
            TakeDamage("B");
        }
        else if (collision.gameObject.CompareTag("Ice"))
        {
            TakeDamage("I");
        }
        else if (collision.gameObject.CompareTag("Poison"))
        {
            TakeDamage("P");
        }
        
    }
    public void TakeDamage(string damageType)
    {
        if (isDead)
        {
            return;
        }



        if (damageType == "B")
        {
            currentHealth -= BloodDamage;
        }
        else if (damageType == "C")
        {
            currentHealth -= ClawsDamage;
        }
        else if (damageType == "I")
        {
            currentHealth -= IceDamage;
        }
        else // poison
        {
            if (isPoisoned)
            {
                return;
            }
            else
            {
                StartCoroutine(Poison());
            }
        }

        if (currentHealth <= 0)
        {
            Death();
        }
    }
    IEnumerator Poison()
    {
        isPoisoned = true;
        float poisonInterval = .1f;
        for (float i = 0f; i < poisonLength; i += poisonInterval)
        {
            yield return new WaitForSeconds(poisonInterval);
            currentHealth -= PoisonDamage;
            if (currentHealth <= 0)
            {
                Death();
                break;
            }
        }
        isPoisoned = false;
    }
    

    void Death()
    {
        isDead = true;
        anim.Play("Death");
        Destroy(gameObject, 1.7f);
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
