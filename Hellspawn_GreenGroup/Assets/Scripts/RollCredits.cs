using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RollCredits : MonoBehaviour
{
    public Image blinder;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadMainMenu());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoadMainMenu()
    {
        for(int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(.1f);
            Color alpha = blinder.color;
            alpha.a += .01f;
            blinder.color = alpha;
        }
        SceneManager.LoadScene(0);
        
    }
}
