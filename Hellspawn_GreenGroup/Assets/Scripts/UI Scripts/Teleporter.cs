using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    private bool alreadyLoading;
    public UpdateUI updateUI;
    public string MessageToDisplay;
    private string LevelBeaten = "You have already beaten this level";
    public int buildIndexofSceneToLoad;

    public bool isIceGate;
    public bool isBloodGate;
    public bool isPoisonGate;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool teleportAvailable = false;
            if (isIceGate)
            {
                teleportAvailable = !(SaveData.Instance.GetHasBeatenIce());
            }
            else if (isBloodGate)
            {
                teleportAvailable = !(SaveData.Instance.GetHasBeatenBlood());
            }
            else if (isPoisonGate)
            {
                teleportAvailable = !(SaveData.Instance.GetHasBeatenPoison());
            }
            else // not any of those
            {
                teleportAvailable = true;
            }
            if (teleportAvailable)
            {
                updateUI.CutOnAndDisplayInteractionText(MessageToDisplay);
                Teleport(other);
            }
            else
            {
                updateUI.CutOnAndDisplayInteractionText(LevelBeaten);
            }
            

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Teleport(other);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            updateUI.CutOffInteractionText();
        }
    }
    void Teleport(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.T) && !alreadyLoading)
        {
            alreadyLoading = true;
            int HPcount = other.gameObject.GetComponent<PlayerInventory>().numHealthPotions;
            int MPcount = other.gameObject.GetComponent<PlayerInventory>().numMagicPotions;
            SaveData.Instance.SetHealthPotionCount(HPcount);
            SaveData.Instance.SetMagicPotionCount(MPcount);
            StartCoroutine(Loading());
        }
    }
    IEnumerator Loading()
    {
        yield return new WaitForSeconds(.01f);
        SceneManager.LoadScene(buildIndexofSceneToLoad);
    }
    // Start is called before the first frame update
    void Start()
    {
        alreadyLoading = false;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
