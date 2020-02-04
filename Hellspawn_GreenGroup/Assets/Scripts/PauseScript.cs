using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseText;
    public GameObject pauseImage;
    public GameObject quitText;
    public GameObject tipMessage;
    private Image pImage;
    private Text pText;
    private Text qText;
    private Text tText;
    public string[] tipMessagesArray;

    private bool paused;
    // Start is called before the first frame update
    void Start()
    {
        pImage = pauseImage.GetComponent<Image>();
        pText = pauseText.GetComponent<Text>();
        qText = quitText.GetComponent<Text>();
        tText = tipMessage.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
        if(paused)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Time.timeScale = 1;
                // reenable any scripts timescale independent

                LoadMainMenu();
            }
        }
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void Pause()
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
            // disable any scripts timescale independent


            var tempColor = pImage.color;
            tempColor.a = .5f;
            pImage.color = tempColor;

            tempColor = pText.color;
            tempColor.a = .5f;
            pText.color = tempColor;

            tempColor = qText.color;
            tempColor.a = .5f;
            qText.color = tempColor;

            int length = tipMessagesArray.GetLength(0);
            // Random.Range with a float is maximally inclusive, but not with an int so no -1 necessary
            int rand = Random.Range(0, (length));
            string msg = "Tip: \n" + tipMessagesArray[rand];
            tempColor = tText.color;
            tempColor.a = .5f;
            tText.color = tempColor;
            tText.text = msg;
        }
        else
        {
            Time.timeScale = 1;            
            // reenable any scripts timescale independent


            var tempColor = pImage.color;
            tempColor.a = 0f;
            pImage.color = tempColor;

            tempColor = pText.color;
            tempColor.a = 0f;
            pText.color = tempColor;

            tempColor = qText.color;
            tempColor.a = 0f;
            qText.color = tempColor;

            tempColor = tText.color;
            tempColor.a = 0f;
            tText.color = tempColor;
            
        }
    }
}
