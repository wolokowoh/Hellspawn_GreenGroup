using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameOverVocals : MonoBehaviour
{
    

    public AudioSource[] retryClips;
    public AudioSource[] deathClips;
    public AudioSource[] quitClips;

    public AudioSource[] GetRetryClips() => retryClips;
    public AudioSource[] GetDeathClips() => deathClips;
    public AudioSource[] GetQuitClips() => quitClips;

    public void playRetry()
    {
        int randDeath = Random.Range(0, GetRetryClips().GetLength(0));
        AudioSource retryClipToPlay = retryClips[randDeath];
        retryClipToPlay.Play();
        StartCoroutine("LoadFromCheckpoint");

    }

    public IEnumerator LoadFromCheckpoint()
    {
        // if we do one save file system, just get last scene

        yield return new WaitForSeconds(7);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void playQuit()
    {
        int randDeath = Random.Range(0, GetQuitClips().GetLength(0));
        AudioSource quitClipToPlay = quitClips[randDeath];
        quitClipToPlay.Play();
        StartCoroutine("LoadMainMenu");
    }

    public IEnumerator LoadMainMenu()
    {
        // if we do one save file system, just get last scene

        yield return new WaitForSeconds(7);
        SceneManager.LoadScene(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
