using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleshLerp : MonoBehaviour
{
    public Material startMaterial;
    public Color endColor;
    public float time;
    public bool rotting;
    private bool coroutineStarted;

    Renderer rend;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Poison"))
        {
            if (!rotting)
            {
                rotting = true;
            }
            Destroy(collision.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rotting = false;
        coroutineStarted = false;
        rend = GetComponent<Renderer>();
        
    }

    public IEnumerator Rot()
    {
        coroutineStarted = true;
        Debug.Log("Starting Rot!");
        float ElapsedTime = 0.0f;
        float TotalTime = time;
        while (ElapsedTime < TotalTime)
        {
            ElapsedTime += Time.deltaTime;
            rend.material.color = Color.Lerp(startMaterial.color, endColor, (ElapsedTime / TotalTime));
            yield return null;
        }
        Debug.Log("Ending Rot!");
        // set the ashes active when we add them

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(rotting && !coroutineStarted)
        {
            StartCoroutine(Rot());
        }
    }
}
