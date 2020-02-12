using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject parent;
    private UpdateUI updateUI;

    // add wherever potions are stored
    public string potionText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            updateUI.CutOnAndDisplayInteractionText(potionText);
            LoadText(other);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadText(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            updateUI.CutOffInteractionText();
        }

    }
    void LoadText(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            bool added = false;
            string pickupName = "";
            // do whatever first
            if (gameObject.CompareTag("MagicPotion"))
            {
                // add the special code here
                added = other.GetComponent<PlayerInventory>().addMagicPotion();
                pickupName = "Magic Potion";
            }
            else if (gameObject.CompareTag("HealthPotion"))
            {
                // add the special code here
                added = other.GetComponent<PlayerInventory>().addHealthPotion();
                pickupName = "Health Potion";
            }


            if (added)
            {
                updateUI.displayPickupMessage(pickupName + " Obtained");
                // maybe add else for life if we do that
                updateUI.CutOffInteractionText();
                Destroy(parent);
            }

        }
    
}
    // Start is called before the first frame update
    void Start()
    {
        updateUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UpdateUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
