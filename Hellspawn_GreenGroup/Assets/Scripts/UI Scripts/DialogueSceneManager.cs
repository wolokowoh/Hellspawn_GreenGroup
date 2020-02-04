using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[System.Serializable]
public class Message
{
   public List<string> message;
}

public class DialogueSceneManager : MonoBehaviour
{
    public DialogueCanvasManager canvasManager;
    
    public List<Message> messagesToDisplay;
    //array indexes when to change character should always be at least 1 less than messages
    public List<int> indexOfCharacterChange;
    // colors for out two talkers
    private int numOfMessages;
    private int lastIndex;
    private int currentIndex;
    private bool loadingScene;
    // Start is called before the first frame update
    void Start()
    {
        loadingScene = false;
        currentIndex = 0;
        numOfMessages = messagesToDisplay.Count;
        lastIndex = numOfMessages - 1;
        canvasManager.SetMessage(messagesToDisplay[currentIndex].message);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (canvasManager.routineRunning)
            {               
                canvasManager.routineRunning = false;
                List<string> message = messagesToDisplay[currentIndex].message;
                StartCoroutine(PrintWhole(message));
            }
            else
            {
                // call nextmesagges
                nextMessage();
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (canvasManager.routineRunning)
            {
                canvasManager.routineRunning = false;
            }
            else if(loadingScene == false)
            {
                loadingScene = true;
                StartCoroutine(LoadNextScene());
                // call LoadScene to load next scene
                
            }
            
        }
    }
    public IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(((SceneManager.GetActiveScene().buildIndex) + 1));
    }
    public IEnumerator PrintWhole(List<string> message)
    {
        yield return new WaitForSeconds(0.01f);
        canvasManager.FinishMessage(message);
    }

    public void nextMessage()
    {
        currentIndex++;
        if (currentIndex > lastIndex)
        {
            // end the scene
            // call LoadScene to load next scene
            if(loadingScene == false)
            {
                loadingScene = true;
                StartCoroutine(LoadNextScene());
            }
            
            
        }
        else
        {
            List<string> message = messagesToDisplay[currentIndex].message;
            // check if character change
            bool characterSwap = false;
            foreach (int index in indexOfCharacterChange)
            {
                if (index == currentIndex)
                {
                    characterSwap = true;
                }
            }
            canvasManager.ChangeMessage(message, characterSwap);
        }
        
    }

}
