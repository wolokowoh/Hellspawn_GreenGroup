using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingIce : MonoBehaviour
{
    public GameObject parent;
    public GameObject platform;
    private Vector3 parentScale;
    private bool coroutineInProgress;
    private bool restoreInProgress;
    public float meltSpeed;
    public float freezeSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!coroutineInProgress && !restoreInProgress)
            {
                StartCoroutine(MeltABitOrDisappear());
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!coroutineInProgress && !restoreInProgress)
            {
                StartCoroutine(MeltABitOrDisappear());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!coroutineInProgress && !restoreInProgress)
            {
                StartCoroutine(RestoreABit());
            }
            else
            {
                StartCoroutine(DelayABit());
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        parentScale = parent.transform.localScale;
        coroutineInProgress = false;
        restoreInProgress = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MeltABitOrDisappear()
    {
        coroutineInProgress = true;
        yield return new WaitForSeconds(meltSpeed);
        if(parent.transform.localScale.y <= .1f)
        {
            // disappear
            if (platform.GetComponent<MeshRenderer>().enabled)
            {
                platform.GetComponent<MeshRenderer>().enabled = false;
                platform.GetComponent<BoxCollider>().enabled = false;
            }
            
        }
        else
        {
            Vector3 scale = parent.transform.localScale;
            scale.y = (scale.y - .1f);
            parent.transform.localScale = scale;
        }
        coroutineInProgress = false;

    }
    IEnumerator RestoreABit()
    {
        restoreInProgress = true;
        yield return new WaitForSeconds(freezeSpeed);
        if (platform.GetComponent<MeshRenderer>().enabled == false)
        {
            platform.GetComponent<MeshRenderer>().enabled = true;
            platform.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            if (parent.transform.localScale.y < parentScale.y)
            {
                Vector3 scale = parent.transform.localScale;
                scale.y = (scale.y + .1f);
                parent.transform.localScale = scale;
            }

        }

        if (parent.transform.localScale.y < parentScale.y)
        {
            StartCoroutine(DelayABit());
        }
        restoreInProgress = false;
    }
    IEnumerator DelayABit()
    {
        yield return new WaitForSeconds(.1f);
        if (!coroutineInProgress && !restoreInProgress)
        {
            StartCoroutine(RestoreABit());
        }
    }
}
