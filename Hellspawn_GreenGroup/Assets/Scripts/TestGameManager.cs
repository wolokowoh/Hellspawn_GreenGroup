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

    private PlayerResistanceExample PlayerResistance;

    // add something update UI vased on playerinventory

    // Start is called before the first frame update
    void Start()
    {
        UIGameOverTrigger = false;
        //hp = GameObject.Find("Player").GetComponent<PlayerController>().health;
        // could add max health here
        updateUI.displayLevelName(levelName);
        
        updateUI.setWeaponToCurrent(ClawsOrb);


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
           && SaveData.Instance.GetPlayerHasIceWeapon()
           )
        {
            // change whatever else you need before updating UI
            PlayerResistance.SetWeaponEquipped(2);
            updateUI.setWeaponToCurrent(IceOrb);
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
            && SaveData.Instance.GetPlayerHasPoisonWeapon()
           )
        {
            // change whatever else you need before updating UI
            PlayerResistance.SetWeaponEquipped(3);
            updateUI.setWeaponToCurrent(PoisonOrb);
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
            && SaveData.Instance.GetPlayerHasBloodWeapon()
           )
        {
            // change whatever else you need before updating UI
            PlayerResistance.SetWeaponEquipped(4);
            updateUI.setWeaponToCurrent(BloodOrb);
        }

        PlayerInventory playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        int hpPotions = playerInventory.numHealthPotions;
        int mpPotions = playerInventory.numMagicPotions;
        updateUI.changeHPRefillText(hpPotions);
        updateUI.changeMPRefillText(mpPotions);

    }

    

}
