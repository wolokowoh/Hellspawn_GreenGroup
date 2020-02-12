using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    private static SaveData _instance;

    private bool hasGameBeenBeaten;

    private bool hasIceWeapon;
    private bool hasBeatenIceLevel;
    
    private bool hasPoisonWeapon;
    private bool hasBeatenPoisonLevel;

    private bool hasBloodWeapon;
    private bool hasBeatenBloodLevel;

    private int HPPotionCount;
    private int MPPotionCount;

    private int indexOfLastLevelLoaded;

    public static SaveData Instance { get { return _instance; } }
    
    

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public int GetHealthPotions() => HPPotionCount;
    public int GetMagicPotions() => MPPotionCount;
    public void SetHealthPotionCount(int count) => HPPotionCount = count;
    public void SetMagicPotionCount(int count) => MPPotionCount = count;

    
    public int GetLastLevel() => indexOfLastLevelLoaded;
    public void SetLastLevel(int currentIndex) => indexOfLastLevelLoaded = currentIndex;

    public bool GetGameBeaten() => hasGameBeenBeaten;
    public void SetGameBeaten(bool trueORFalse) => hasGameBeenBeaten = trueORFalse;

    public bool GetPlayerHasIceWeapon() => hasIceWeapon;
    public bool GetPlayerHasBloodWeapon() => hasBloodWeapon;
    public bool GetPlayerHasPoisonWeapon() => hasPoisonWeapon;
    public void SetPlayerHasIceWeapon(bool trueORFalse) => hasIceWeapon = trueORFalse;
    public void SetPlayerHasBloodWeapon(bool trueORFalse) => hasBloodWeapon = trueORFalse;
    public void SetPlayerHasPoisonWeapon(bool trueORFalse) => hasPoisonWeapon = trueORFalse;

    public bool GetHasBeatenIce() => hasBeatenIceLevel;
    public bool GetHasBeatenBlood() => hasBeatenBloodLevel;
    public bool GetHasBeatenPoison() => hasBeatenPoisonLevel;
    public void SetHasBeatenIce(bool trueORFalse) => hasBeatenIceLevel = trueORFalse;
    public void SetHasBeatenBlood(bool trueORFalse) => hasBeatenBloodLevel = trueORFalse;
    public void SetHasBeatenPoison(bool trueORFalse) => hasBeatenPoisonLevel = trueORFalse;


}
