using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    private static SaveData _instance;

    private bool hasGameBeenBeaten;
    private bool hasIceWeapon;
    private bool hasPoisonWeapon;
    private bool hasBloodWeapon;
    private int HPPotionCount;
    private int MPPotionCount;

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

    public void SetHealthPotionCount(int count) => HPPotionCount = count;
    public void SetMagicPotionCount(int count) => MPPotionCount = count;

    public int GetHealthPotions() => HPPotionCount;
    public int GetMagicPotions() => MPPotionCount;



    public bool GetGameBeaten() => hasGameBeenBeaten;
    public bool GetPlayerHasIceWeapon() => hasIceWeapon;
    public bool GetPlayerHasBloodWeapon() => hasBloodWeapon;
    public bool GetPlayerHasPoisonWeapon() => hasPoisonWeapon;

    public void SetGameBeaten(bool trueORFalse) => hasGameBeenBeaten = trueORFalse;
    public void SetPlayerHasIceWeapon(bool trueORFalse) => hasIceWeapon = trueORFalse;
    public void SetPlayerHasBloodWeapon(bool trueORFalse) => hasBloodWeapon = trueORFalse;
    public void SetPlayerHasPoisonWeapon(bool trueORFalse) => hasPoisonWeapon = trueORFalse;

}
