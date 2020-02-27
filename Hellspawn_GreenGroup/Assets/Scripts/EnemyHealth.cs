using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public bool isWarden;

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


    // warden specific 
    public int startingBloodHealth = 100;
    public int currentBloodHealth;
    public int startingPoisonHealth = 100;
    public int currentPoisonHealth;
    public int startingIceHealth = 100;
    public int currentIceHealth;
    private ResistancesStructure BloodStageDamages;
    private ResistancesStructure PoisonStageDamages;
    private ResistancesStructure IceStageDamages;

    void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        if (isWarden)
        {
            BloodStageDamages.SetBloodResistance(0);
            BloodStageDamages.SetFrostResistance(2);
            BloodStageDamages.SetPoisonResistance(3);// SEEMS LOW but its 30 damage poison kills blood
            BloodStageDamages.SetBruteResistance(10);

            PoisonStageDamages.SetPoisonResistance(0);
            PoisonStageDamages.SetFrostResistance(20); // frost kills poison
            PoisonStageDamages.SetBloodResistance(6);
            PoisonStageDamages.SetBruteResistance(10);

            IceStageDamages.SetBruteResistance(5);
            IceStageDamages.SetFrostResistance(0);
            IceStageDamages.SetPoisonResistance(0);
            IceStageDamages.SetBloodResistance(20);// BLOOD kill Ice

            currentBloodHealth = startingBloodHealth;
            currentIceHealth = startingIceHealth;
            currentPoisonHealth = startingPoisonHealth;
        }
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

        if (!isWarden)
        {
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
        else// warden stuff
        {
            if (damageType == "B")
            {
                if(currentBloodHealth > 0)
                {
                    currentBloodHealth -= BloodStageDamages.GetBloodResistance();
                }
                else if(currentPoisonHealth >0)
                {
                    currentPoisonHealth -= PoisonStageDamages.GetBloodResistance();
                }   
                else if(currentIceHealth > 0)
                {
                    currentIceHealth -= IceStageDamages.GetBloodResistance();
                }
                
                 
            }
            else if (damageType == "C")
            {
                if (currentBloodHealth > 0)
                {
                    currentBloodHealth -= BloodStageDamages.GetBruteResistance();
                }
                else if (currentPoisonHealth > 0)
                {
                    currentPoisonHealth -= PoisonStageDamages.GetBruteResistance();
                }
                else if (currentIceHealth > 0)
                {
                    currentIceHealth -= IceStageDamages.GetBruteResistance();
                }
            }
            else if (damageType == "I")
            {
                if (currentBloodHealth > 0)
                {
                    currentBloodHealth -= BloodStageDamages.GetFrostResistance();
                }
                else if (currentPoisonHealth > 0)
                {
                    currentPoisonHealth -= PoisonStageDamages.GetFrostResistance();
                }
                else if (currentIceHealth > 0)
                {
                    currentIceHealth -= IceStageDamages.GetFrostResistance();
                }
            }
            else // poison
            {
                if (isPoisoned)
                {
                    return;
                }
                else
                {
                    if (currentBloodHealth > 0) { 
                        StartCoroutine(Poison());
                    }
                }
            }

            if (currentIceHealth <= 0)
            {
                Death();
            }
        }

        

        
    }
    IEnumerator Poison()
    {
        isPoisoned = true;
        float poisonInterval = .1f;
        if (!isWarden)
        {      
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
        }
        else
        {
            for (float i = 0f; i < poisonLength; i += poisonInterval)
            {
                yield return new WaitForSeconds(poisonInterval);
                currentBloodHealth -= BloodStageDamages.GetPoisonResistance();
                if (currentBloodHealth <= 0)
                {
                    break;
                }
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
