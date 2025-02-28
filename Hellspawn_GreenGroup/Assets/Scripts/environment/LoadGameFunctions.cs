﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameFunctions : MonoBehaviour
{
    public Button newGamePlus;
    public Button newGame;
    public Button loadGame;
    public Button controls;
    public Button goBack;
    public Button quitButton;

    public static GameObject menuDisplayObject;
    public static GameObject controlsDisplayObject;

    
    
    public UnityEngine.Events.UnityAction load = () => { LoadSceneFromSave(); };
    UnityEngine.Events.UnityAction newSavePlus = () => { NewGamePlus(); };
    UnityEngine.Events.UnityAction newSave = () => { NewGame(); };
    UnityEngine.Events.UnityAction showControlsMenu = () => { showControlsDisplay(); };
    UnityEngine.Events.UnityAction showMainMenu = () => { showMenuDisplay(); };
    UnityEngine.Events.UnityAction exit = () => { Quit(); };

    private void Start()
    {
        newGamePlus.GetComponent<Button>().onClick.AddListener(newSavePlus);
        loadGame.GetComponent<Button>().onClick.AddListener(load);
        newGame.GetComponent<Button>().onClick.AddListener(newSave);
        controls.GetComponent<Button>().onClick.AddListener(showControlsMenu);
        goBack.GetComponent<Button>().onClick.AddListener(showMainMenu);
        quitButton.GetComponent<Button>().onClick.AddListener(exit);
        menuDisplayObject = GameObject.FindGameObjectWithTag("MenuDisplay");
        controlsDisplayObject = GameObject.FindGameObjectWithTag("ControlsDisplay");
        menuDisplayObject.SetActive(true);
        controlsDisplayObject.SetActive(false);
        if(SaveData.Instance.GetGameBeaten() == false)
        {
            newGamePlus.gameObject.SetActive(false);
        }
    }
    public static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }
    public static void LoadSceneFromSave()
    {
        // set up a save system or just load the last scene/checkpoint
        int level = SaveData.Instance.GetLastLevel();
        if (level == 0)
        {
            level = 1;
        }

        // for now just load the hub
        SceneManager.LoadScene(level);
    }
    public static void NewGamePlus()
    {
        // setup the bools in SavaData
        SaveData.Instance.SetPlayerHasBloodWeapon(true);
        SaveData.Instance.SetPlayerHasIceWeapon(true);
        SaveData.Instance.SetPlayerHasPoisonWeapon(true);
        SetCommonNewGameParameters();
        // for now just load opening cut
        SceneManager.LoadScene("OpeningCut");
    }
    public static void NewGame()
    {
        // setup the bools in SavaData
        SaveData.Instance.SetPlayerHasBloodWeapon(false);
        SaveData.Instance.SetPlayerHasIceWeapon(false);
        SaveData.Instance.SetPlayerHasPoisonWeapon(false);


        SetCommonNewGameParameters();

        // load opening scene
        SaveData.Instance.SetHealthPotionCount(0);
        SaveData.Instance.SetMagicPotionCount(0);

        // for now just load SampleScene 
        SceneManager.LoadScene("OpeningCut");
    }

    public static void SetCommonNewGameParameters()
    {
        SaveData.Instance.SetHasBeatenBlood(false);
        SaveData.Instance.SetHasBeatenIce(false);
        SaveData.Instance.SetHasBeatenPoison(false);
        SaveData.Instance.SetGameBeaten(false);
    }
    public static void showControlsDisplay()
    {
        menuDisplayObject.SetActive(false);
        controlsDisplayObject.SetActive(true);
    }
    public static void showMenuDisplay()
    {
        menuDisplayObject.SetActive(true);
        controlsDisplayObject.SetActive(false);
    }

}
