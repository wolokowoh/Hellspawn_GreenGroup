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

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                // do whatever first
                if (gameObject.CompareTag("MagicPotion"))
                {
                    // add the special code here
                }
                else if (gameObject.CompareTag("HealthPotion"))
                {
                    // add the special code here
                }
                // maybe add else for life if we do that
                updateUI.CutOffInteractionText();
                Destroy(parent);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            updateUI.CutOffInteractionText();
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
