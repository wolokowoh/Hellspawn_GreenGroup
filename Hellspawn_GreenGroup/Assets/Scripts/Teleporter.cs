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
            Teleport(other);

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
