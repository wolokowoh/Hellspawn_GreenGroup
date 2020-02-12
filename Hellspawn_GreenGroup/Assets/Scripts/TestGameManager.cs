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
    public int hp = 100;
    public int maxHP = 100;
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

        PlayerInventory playerInventory = 
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        int hpPotions = playerInventory.numHealthPotions;
        int mpPotions = playerInventory.numMagicPotions;
        updateUI.changeHPRefillText(hpPotions);
        updateUI.changeMPRefillText(mpPotions);

        hp = controller.health;
        updateUI.changeHealthSliderValue(hp, maxHP);

    }

    

}
