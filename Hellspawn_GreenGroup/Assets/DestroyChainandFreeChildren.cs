using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChainandFreeChildren : MonoBehaviour
{
    public GameObject[] objectsAttachtoChain;
    public GameObject[] chainLinks;
    bool destroyed = false;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Blood") && !destroyed)
        {
            destroyed = true;
            if (objectsAttachtoChain.Length != 0)
            {
                for (int i = 0; i < objectsAttachtoChain.Length; i++)
                {
                    Rigidbody rb = objectsAttachtoChain[i].GetComponent<Rigidbody>();
                    rb.isKinematic = false;
                    rb.useGravity = true;

                }
            }
            for (int i = 0; i < chainLinks.Length; i++)
            {
                Rigidbody rb = chainLinks[i].GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;

            }
            gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(DestroyedChain());
        }
        
    }

    IEnumerator DestroyedChain()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        
    }

}
