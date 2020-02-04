using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ResistancesStructure
{
    // SOME OF THESE ARE BASED OFF TYLER'S TRIGGER
    private int brnRST;
    private int fstRST;
    private int bruRST;
    private int bldRST;
    private int psnRST;
    private int shckRST;

    public int GetBurnResistance() => brnRST;
    public int GetFrostResistance() => fstRST;
    public int GetBruteResistance() => bruRST;
    public int GetBloodResistance() => bldRST;
    public int GetPoisonResistance() => psnRST;
    public int GetShockResistance() => shckRST;

    public void SetBurnResistance(int burnRST) => brnRST = burnRST;
    public void SetFrostResistance(int frostRST) => fstRST = frostRST;
    public void SetBruteResistance(int bruteRST) => bruRST = bruteRST;
    public void SetBloodResistance(int bloodRST) => bldRST = bloodRST;
    public void SetPoisonResistance(int poisonRST) => psnRST = poisonRST;
    public void SetShockResistance(int shockRST) => shckRST = shockRST;
}
