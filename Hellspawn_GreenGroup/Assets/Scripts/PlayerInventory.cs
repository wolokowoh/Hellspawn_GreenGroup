using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int numHealthPotions;
    public int numMagicPotions;

    public int maxPotions;


    public bool addHealthPotion()
    {
        if(numHealthPotions == maxPotions)
        {
            return false;
        }

        numHealthPotions++;
        return true;
    }
    public bool addMagicPotion()
    {
        if (numMagicPotions == maxPotions)
        {
            return false;
        }

        numMagicPotions++;
        return true;
    }

    public void getCounts()
    {
        numMagicPotions = SaveData.Instance.GetMagicPotions();
        numHealthPotions = SaveData.Instance.GetHealthPotions();
    }
    private void Start()
    {
        getCounts();
    }
}
