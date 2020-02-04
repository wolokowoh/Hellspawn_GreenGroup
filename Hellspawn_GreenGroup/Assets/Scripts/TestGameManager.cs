using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    //public PlayerController playerController;
    
    public UpdateUI updateUI;
    public int hp = 50;
    public int maxHP = 100;
    public int hpRefillCount = 10;

    public GameObject BloodOrb;
    public GameObject PoisonOrb;
    public GameObject ClawsOrb;
    public GameObject IceOrb;
    public bool UIGameOverTrigger;
    public string levelName;
    public string pickupName;

    public PlayerResistanceExample PlayerResistance;

    string msg = "This is the test message. If you are seeing this, \n"
            + "I guess that this is working as intended.";
    string msg2 = "Also a test message.";

    // Start is called before the first frame update
    void Start()
    {

        UIGameOverTrigger = false;
        //hp = GameObject.Find("Player").GetComponent<PlayerController>().health;
        // could add max health here
        updateUI.displayLevelName(levelName);
        updateUI.displayPickupMessage(pickupName + " Obtained");
        updateUI.setWeaponToCurrent(ClawsOrb);

        // sets which weapons we allow the player to equip
        SaveData.Instance.setPlayerHasBloodWeapon(false);
        SaveData.Instance.setPlayerHasIceWeapon(true);
        SaveData.Instance.setPlayerHasPoisonWeapon(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (UIGameOverTrigger)
        {
            UIGameOverTrigger = false;
            updateUI.StartCoroutine("GameOver");
        }
        updateUI.changeHealthSliderValue(hp, maxHP);

        // note putting the following in start causes an error
        updateUI.changeHPRefillText(hpRefillCount);

        // here some change weapon methods

        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            // change whatever else you need before updating UI
            PlayerResistance.SetWeaponEquipped(1);
            updateUI.setWeaponToCurrent(ClawsOrb);
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
           && SaveData.Instance.getPlayerHasIceWeapon()
           )
        {
            // change whatever else you need before updating UI
            PlayerResistance.SetWeaponEquipped(2);
            updateUI.setWeaponToCurrent(IceOrb);
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
            && SaveData.Instance.getPlayerHasPoisonWeapon()
           )
        {
            // change whatever else you need before updating UI
            PlayerResistance.SetWeaponEquipped(3);
            updateUI.setWeaponToCurrent(PoisonOrb);
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
            && SaveData.Instance.getPlayerHasBloodWeapon()
           )
        {
            // change whatever else you need before updating UI
            PlayerResistance.SetWeaponEquipped(4);
            updateUI.setWeaponToCurrent(BloodOrb);
        }


        // test for structure to make sure enemies and players can use it as a variable
        ResistancesStructure resistancesStructure = new ResistancesStructure();
        resistancesStructure.SetBloodResistance(5);
        hp = resistancesStructure.GetBloodResistance();

        if(updateUI.Dialogue.activeInHierarchy && Input.GetKeyDown(KeyCode.T))
        {
            if (updateUI.getCurrentDialogueString() == msg)
            {
                updateUI.changeDialogTextToNextMessage(msg2);
            }
            else // In this case its msg two
            {
                updateUI.cutOffDialogue();
            }
        }
        else if(Input.GetKeyDown(KeyCode.T))
        {
            updateUI.cutOnDialogueAndSetText(msg);
        }

    }

    

}
