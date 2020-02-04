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

    public bool getGameBeaten() { return hasGameBeenBeaten; }
    public bool getPlayerHasIceWeapon() { return hasIceWeapon; }
    public bool getPlayerHasBloodWeapon() { return hasBloodWeapon; }
    public bool getPlayerHasPoisonWeapon() { return hasPoisonWeapon; }

    public void setGameBeaten(bool trueORFalse) { hasGameBeenBeaten = trueORFalse; }
    public void setPlayerHasIceWeapon(bool trueORFalse) { hasIceWeapon = trueORFalse; }
    public void setPlayerHasBloodWeapon(bool trueORFalse) { hasBloodWeapon = trueORFalse; }
    public void setPlayerHasPoisonWeapon(bool trueORFalse) { hasPoisonWeapon = trueORFalse; }

}
