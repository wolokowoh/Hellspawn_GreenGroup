using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnlocks : MonoBehaviour
{
    public string unlockCode;
    public EnemyHealth boss;
    public GameObject gate;
    // Start is called before the first frame update

    private void Awake()
    {
        if(unlockCode == "W")
        {
            gate.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(unlockCode == "W")
        {
            if(boss.currentIceHealth <=0)
            {
                Unlock();
                gameObject.SetActive(false);
            }
        }
        else if(boss.currentHealth <= 0)
        {
            Unlock();
            gameObject.SetActive(false);
        }
    }
    public void Unlock()
    {
        if(unlockCode == "I")
        {
            SaveData.Instance.SetPlayerHasIceWeapon(true);
            SaveData.Instance.SetHasBeatenIce(true);
        }
        else if(unlockCode == "B")
        {
            SaveData.Instance.SetPlayerHasBloodWeapon(true);
            SaveData.Instance.SetHasBeatenBlood(true);
        }
        else if (unlockCode == "P") // poison
        {
            SaveData.Instance.SetPlayerHasPoisonWeapon(true);
            SaveData.Instance.SetHasBeatenPoison(true);
        }
        else // warden
        {
            SaveData.Instance.SetGameBeaten(true);
            gate.SetActive(true);
        }
    }
}
