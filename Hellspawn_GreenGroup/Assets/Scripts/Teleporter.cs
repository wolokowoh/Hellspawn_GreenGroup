using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    private bool alreadyLoading;
    public UpdateUI updateUI;
    public string MessageToDisplay;
    public int buildIndexofSceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            updateUI.CutOnAndDisplayInteractionText(MessageToDisplay);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            updateUI.CutOffInteractionText();
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
