using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestGameManager : MonoBehaviour
{
    //public PlayerController playerController;
    public GameObject playerPrefab;
    public GameObject cameraPrefab;
    public Vector3 playerSpawn;
    public UpdateUI updateUI;
    private int hp;
    public int maxHP = 100;
    public int maxBlood = 100;
    public int maxIce = 100;
    public int maxPoison= 100;

    public int hpRefillCount = 10;

    public GameObject BloodOrb;
    public GameObject PoisonOrb;
    public GameObject ClawsOrb;
    public GameObject IceOrb;
    public bool UIGameOverTrigger;
    public bool gameOver; // prevents the collider from calling the coroutine repeatedly
    public string levelName;
    private PlayerResistanceExample PlayerResistance;
    PlayerController controller;

    // add something update UI vased on playerinventory

    // Start is called before the first frame update
    void Start()
    {

        SaveData.Instance.SetLastLevel(SceneManager.GetActiveScene().buildIndex);
        // comment these out
        SaveData.Instance.SetPlayerHasBloodWeapon(true);
        SaveData.Instance.SetPlayerHasIceWeapon(true);

        gameOver = false;
        UIGameOverTrigger = false;
        //hp = GameObject.Find("Player").GetComponent<PlayerController>().health;
        // could add max health here
        updateUI.displayLevelName(levelName);
        
        updateUI.setWeaponToCurrent(ClawsOrb);

        Instantiate(playerPrefab, playerSpawn, playerPrefab.transform.rotation);
        Instantiate(cameraPrefab, cameraPrefab.transform.position, cameraPrefab.transform.rotation);
        controller =
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
        controller.SetMaxHealth(maxHP);
        controller.SetMaxBlood(maxBlood);
        controller.SetMaxIce(maxIce);
        controller.SetMaxPoison(maxPoison);
        PlayerResistance = 
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResistanceExample>();


    }

    // Update is called once per frame
    void Update()
    {
        if (UIGameOverTrigger)
        {
            UIGameOverTrigger = false;
            if (!gameOver)
            {
                gameOver = true;
                updateUI.StartCoroutine("GameOver");
            }
            
        }
        updateUI.changeHealthSliderValue(hp, maxHP);

        // note putting the following in start causes an error
        updateUI.changeHPRefillText(hpRefillCount);
        PlayerInventory playerInventory =
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        int hpPotions = playerInventory.numHealthPotions;
        int mpPotions = playerInventory.numMagicPotions;
        updateUI.changeHPRefillText(hpPotions);
        updateUI.changeMPRefillText(mpPotions);

        hp = controller.health;
        updateUI.changeHealthSliderValue(hp, maxHP);

        // here some change weapon methods

        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            // change whatever else you need before updating UI
            PlayerResistance.SetWeaponEquipped(1);
            updateUI.setWeaponToCurrent(ClawsOrb);
            controller.currentWeapon = Weapon.Claws;
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
           && SaveData.Instance.GetPlayerHasIceWeapon()
           )
        {
            // change whatever else you need before updating UI
            PlayerResistance.SetWeaponEquipped(2);
            updateUI.setWeaponToCurrent(IceOrb);
            controller.currentWeapon = Weapon.Ice;
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
            && SaveData.Instance.GetPlayerHasPoisonWeapon()
           )
        {
            // change whatever else you need before updating UI
            PlayerResistance.SetWeaponEquipped(3);
            updateUI.setWeaponToCurrent(PoisonOrb);
            controller.currentWeapon = Weapon.Poison;
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
            && SaveData.Instance.GetPlayerHasBloodWeapon()
           )
        {
            // change whatever else you need before updating UI
            PlayerResistance.SetWeaponEquipped(4);
            updateUI.setWeaponToCurrent(BloodOrb);
            controller.currentWeapon = Weapon.Blood;
        }

        if(controller.currentWeapon == Weapon.Claws)
        {
            // do no updates to ui
        }
        else if(controller.currentWeapon == Weapon.Blood)
        {
            updateUI.changeBloodSliderValue(controller.bloodBarCurrentMP, controller.bloodBarMaxMP);
        }
        else if (controller.currentWeapon == Weapon.Poison)
        {
            updateUI.changePoisonSliderValue(controller.poisonBarCurrentMP, controller.poisonBarMaxMP);
        }
        else if (controller.currentWeapon == Weapon.Ice)
        {
            updateUI.changeIceSliderValue(controller.iceBarCurrentMP, controller.iceBarMaxMP);
        }
    }

    

}
