using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthExample : MonoBehaviour
{
    ResistancesStructure enemyResistancesExample = new ResistancesStructure();
    public int BloodResistance, BruteResistance, BurnResistance,
        FrostResistance, PoisonResistance, ShockResistance;


    // Start is called before the first frame update
    void Start()
    {
        enemyResistancesExample.SetBloodResistance(BloodResistance);
        enemyResistancesExample.SetBruteResistance(BruteResistance);
        enemyResistancesExample.SetBurnResistance(BurnResistance);
        enemyResistancesExample.SetFrostResistance(FrostResistance);
        enemyResistancesExample.SetPoisonResistance(PoisonResistance);
        enemyResistancesExample.SetShockResistance(ShockResistance);

        // when enemy damaged you can pass whole structure if necessary to a damage calculator
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    
}
