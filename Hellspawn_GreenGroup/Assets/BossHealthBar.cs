using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public bool BossFightTime;
    private UpdateUI updateUI;
    private EnemyHealth health;
    public string bossCode;
    public void cutOnBar()
    {
        if (bossCode == "W")
        {
            if (!GameObject.FindGameObjectWithTag("WardenBossBar").activeInHierarchy)
            {
                GameObject.FindGameObjectWithTag("WardenBossBar").SetActive(true);
            }
        }
        else if (bossCode == "I")
        {
            if (!GameObject.FindGameObjectWithTag("IceBossBar").activeInHierarchy)
            {
                GameObject.FindGameObjectWithTag("IceBossBar").SetActive(true);
            }
        }
        else if (bossCode == "B")
        {
            if (!GameObject.FindGameObjectWithTag("BloodBossBar").activeInHierarchy)
            {
                GameObject.FindGameObjectWithTag("BloodBossBar").SetActive(true);
            }
        }
        else // p for poison
        {
            if (!GameObject.FindGameObjectWithTag("PoisonBossBar").activeInHierarchy)
            {
                GameObject.FindGameObjectWithTag("PoisonBossBar").SetActive(true);
            }
        }
        BossFightTime = true;
    }
    // Start is called before the first frame update
    void Start()
    {

        BossFightTime = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (BossFightTime)
        {
            updateUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UpdateUI>();

            if (bossCode == "W")
            {
                if (health.currentIceHealth <= 0)
                {
                    health.currentIceHealth = 0;
                }
                if (health.currentBloodHealth < 0)
                {
                    health.currentBloodHealth = 0;
                }

                if (health.currentPoisonHealth < 0)
                {
                    health.currentPoisonHealth = 0;
                }
                updateUI.changeWardenBloodSliderValue(health.currentBloodHealth, health.startingBloodHealth);
                updateUI.changeWardenPoisonSliderValue(health.currentPoisonHealth, health.startingPoisonHealth);
                updateUI.changeWardenIceSliderValue(health.currentIceHealth, health.startingIceHealth);

            }
            else if (bossCode == "I")
            {
                health = GetComponent<EnemyHealth>();
                updateUI.changeIceBossHealthSliderValue(health.currentHealth, health.startingHealth);

            }
            else if (bossCode == "B")
            {
                health = GetComponent<EnemyHealth>();
                updateUI.changeBloodBossHealthSliderValue(health.currentHealth, health.startingHealth);

            }
            else // p for poison
            {
                health = GetComponent<EnemyHealth>();
                updateUI.changePoisonBossHealthSliderValue(health.currentHealth, health.startingHealth);

            }
        }
    }
}
