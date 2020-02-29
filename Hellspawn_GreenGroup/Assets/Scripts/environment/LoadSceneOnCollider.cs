using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnCollider : MonoBehaviour
{
    TestGameManager testGameManager;
    bool gameOver;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !gameOver)
        {
            gameOver = true;
            // PENALTY
            other.gameObject.GetComponent<PlayerInventory>().getCounts();
            other.gameObject.GetComponent<PlayerController>().SetCurrentHealth(0);
            testGameManager.UIGameOverTrigger = true;
            
            
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        testGameManager =
            GameObject.FindGameObjectWithTag("TGManager").GetComponent<TestGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
