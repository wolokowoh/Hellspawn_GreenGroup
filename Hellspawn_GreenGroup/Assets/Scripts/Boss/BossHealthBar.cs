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
        updateUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UpdateUI>();
        if (bossCode == "W")
        {
            updateUI.cutOnWardenBossHealth();
        }
        else if (bossCode == "I")
        {
            updateUI.cutOnIceBossHealth();
        }
        else if (bossCode == "B")
        {
            updateUI.cutOnBloodBossHealth();
        }
        else // p for poison
        {
            updateUI.cutOnPoisonBossHealth();
        }
        BossFightTime = true;
    }
    // Start is called before the first frame update
    void Start()
    {

        BossFightTime = false;

    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (BossFightTime)
        {
            updateUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UpdateUI>();
            health = GetComponent<EnemyHealth>();
            if (bossCode == "W")
            {
                if (health.currentIceHealth <= 0)
                {
                    health.currentIceHealth = 0;
                    updateUI.cutOffWardenBossHealth();
                    
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
                if(health.currentHealth <= 0)
                {
                    updateUI.cutOffIceBossHealth();
                }

            }
            else if (bossCode == "B")
            {
                health = GetComponent<EnemyHealth>();
                updateUI.changeBloodBossHealthSliderValue(health.currentHealth, health.startingHealth);
                if (health.currentHealth <= 0)
                {
                    updateUI.cutOffBloodBossHealth();
                }

            }
            else // p for poison
            {
                health = GetComponent<EnemyHealth>();
                updateUI.changePoisonBossHealthSliderValue(health.currentHealth, health.startingHealth);
                if (health.currentHealth <= 0)
                {
                    updateUI.cutOffPoisonBossHealth();
                }

            }
        }
    }
}
