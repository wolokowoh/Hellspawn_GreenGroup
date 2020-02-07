using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    private UpdateUI updateUI;
    public string NPCname;

    public List<Message> messages;
    private int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        updateUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UpdateUI>();
        currentIndex = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            updateUI.CutOnAndDisplayInteractionText("Press T to Talk To " + NPCname);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (updateUI.interaction.activeInHierarchy)
                {
                    updateUI.CutOffInteractionText();
                }
                
                if (currentIndex == 0)
                {
                    List<string> message = messages[currentIndex].message;
                    string display = "";
                    foreach (string line in message)
                    {
                        display += line;
                        display += "\n";
                    }
                    updateUI.cutOnDialogueAndSetText(display);
                }
                else if (currentIndex < messages.Count)
                {
                    List<string> message = messages[currentIndex].message;
                    string display = "";
                    foreach (string line in message)
                    {
                        display += line;
                        display += "\n";
                    }
                    updateUI.changeDialogTextToNextMessage(display);
                }
                else
                {
                    updateUI.cutOffDialogue();
                    currentIndex = -1; // to make it 0
                }
                currentIndex++;
                if(currentIndex == 0)
                {
                    updateUI.CutOnAndDisplayInteractionText("Press T to Talk To " + NPCname);
                }
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (updateUI.interaction.activeInHierarchy)
            {
                updateUI.CutOffInteractionText();
            }
            if (updateUI.Dialogue.activeInHierarchy)
            {
                updateUI.cutOffDialogue();
            }
        }
            
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
