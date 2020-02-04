using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResistanceExample : MonoBehaviour
{
    public int weaponEquipped;
    public int resistanceToShowInEditorFoTest;

    ResistancesStructure playerResistancesExample = new ResistancesStructure();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetPlayerResistance();
        // 1, 2, 3, 4 see TestGameManager for this being changed or change it using weaponEquipped
        resistanceToShowInEditorFoTest = playerResistancesExample.GetShockResistance();
    }
    // 1,2,3,4 same as TestManager
    public void SetWeaponEquipped(int weaponNum) => weaponEquipped = weaponNum;

    public void SetPlayerResistance()
    {
        switch (weaponEquipped) // we're making this 1-4
        {
            case 1: // claws equipped
                playerResistancesExample.SetBloodResistance(0);
                playerResistancesExample.SetBruteResistance(10);
                playerResistancesExample.SetBurnResistance(5);
                playerResistancesExample.SetFrostResistance(5);
                playerResistancesExample.SetPoisonResistance(5);
                playerResistancesExample.SetShockResistance(1);
                break;
            case 2:// ice equipped
                playerResistancesExample.SetBloodResistance(5);
                playerResistancesExample.SetBruteResistance(5);
                playerResistancesExample.SetBurnResistance(0);
                playerResistancesExample.SetFrostResistance(10);
                playerResistancesExample.SetPoisonResistance(5);
                playerResistancesExample.SetShockResistance(2);
                break;
            case 3:// poison equipped
                playerResistancesExample.SetBloodResistance(5);
                playerResistancesExample.SetBruteResistance(5);
                playerResistancesExample.SetBurnResistance(5);
                playerResistancesExample.SetFrostResistance(0);
                playerResistancesExample.SetPoisonResistance(10);
                playerResistancesExample.SetShockResistance(3);
                break;
            case 4:// blood equipped
                playerResistancesExample.SetBloodResistance(10);
                playerResistancesExample.SetBruteResistance(0);
                playerResistancesExample.SetBurnResistance(5);
                playerResistancesExample.SetFrostResistance(5);
                playerResistancesExample.SetPoisonResistance(5);
                playerResistancesExample.SetShockResistance(4);

                break;
            default:
                // claws equipped
                playerResistancesExample.SetBloodResistance(0);
                playerResistancesExample.SetBruteResistance(10);
                playerResistancesExample.SetBurnResistance(5);
                playerResistancesExample.SetFrostResistance(5);
                playerResistancesExample.SetPoisonResistance(5);
                playerResistancesExample.SetShockResistance(1);
                break;
        }
    }


}
