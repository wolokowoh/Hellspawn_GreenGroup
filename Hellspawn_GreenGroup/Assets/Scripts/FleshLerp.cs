using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleshLerp : MonoBehaviour
{
    public Material startMaterial;
    public Color endColor;
    public float time;
    public bool rotting;

    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rotting = false;
        rend = GetComponent<Renderer>();
        StartCoroutine(Rot());
        
    }

    public IEnumerator Rot()
    {
        Debug.Log("Starting Infestation!");
        float ElapsedTime = 0.0f;
        float TotalTime = time;
        while (ElapsedTime < TotalTime)
        {
            ElapsedTime += Time.deltaTime;
            rend.material.color = Color.Lerp(startMaterial.color, endColor, (ElapsedTime / TotalTime));
            yield return null;
        }
        Debug.Log("Ending Infestation!");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
