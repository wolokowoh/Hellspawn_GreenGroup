using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueCanvasManager : MonoBehaviour
{
    public Image blinder;
    public Text textToDisplay;
    public int defaultCharacter;
    public Color AbraxasColor;
    public Color OtherPersonColor;
    private Color currentColor;
    public float printSpeed = 0.01f;
    public bool routineRunning;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMessage(List<string> firstMessage)
    {
        switch (defaultCharacter)
        {
            case 0:
                currentColor = AbraxasColor;
                break;
            case 1:
                currentColor = OtherPersonColor;
                break;
            default:
                currentColor = AbraxasColor;
                break;
        }
        textToDisplay.color = currentColor;
        textToDisplay.text = "";
        StartCoroutine(MessagePrint(printSpeed, firstMessage));

    }
    public void changeMessageColor()
    {
        if(currentColor == AbraxasColor)
        {
            currentColor = OtherPersonColor;
        }
        else
        {
            currentColor = AbraxasColor;
        }
        textToDisplay.color = currentColor;
    }
    public void ChangeMessage(List<string> message, bool characterChange)
    {
        // empty TextBox
        textToDisplay.text = "";
        if (characterChange)
        {
            changeMessageColor();
        }
        StartCoroutine(MessagePrint(printSpeed, message));
    }
    public void FinishMessage(List<string> message) // call this if interrupted coroutine
    {
        textToDisplay.text = "";
        int i = 0;
        foreach (string line in message)
        {

            if (i > 0)
            {
                string temporary = textToDisplay.text;
                temporary += "\n";
                textToDisplay.text = temporary;
            }
            foreach (char a in line)
            {
                string temp = textToDisplay.text;
                temp += a;
                textToDisplay.text = temp;
            }
            i++;

        }
    }
    public IEnumerator MessagePrint(float printSpeed, List<string> message)
    {
        routineRunning = true;
        int i = 0;
        foreach (string line in message)
        {
            if (routineRunning == false)
            {
                yield break;
            }
            if(i > 0)
            {
                string temporary = textToDisplay.text;
                temporary += "\n";
                textToDisplay.text = temporary;
            }
            foreach (char a in line)
            {
                if (routineRunning == false)
                {
                    yield break;
                }
                yield return new WaitForSeconds(printSpeed);
                string temp = textToDisplay.text;
                temp += a;
                textToDisplay.text = temp;
                
            }
            i++;

        }

        routineRunning = false;
    }


}
