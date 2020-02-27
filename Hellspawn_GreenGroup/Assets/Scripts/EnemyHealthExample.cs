using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthExample : MonoBehaviour
{
    ResistancesStructure enemyResistancesExample = new ResistancesStructure();
    public int BloodResistance, BruteResistance,
        FrostResistance, PoisonResistance;


    // Start is called before the first frame update
    void Start()
    {
        enemyResistancesExample.SetBloodResistance(BloodResistance);
        enemyResistancesExample.SetBruteResistance(BruteResistance);
        enemyResistancesExample.SetFrostResistance(FrostResistance);
        enemyResistancesExample.SetPoisonResistance(PoisonResistance);

        // when enemy damaged you can pass whole structure if necessary to a damage calculator
    }
    public ResistancesStructure getResistances()
    {
        return enemyResistancesExample;
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    
    
}
