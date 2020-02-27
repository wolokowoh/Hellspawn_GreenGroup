using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChainandFreeChildren : MonoBehaviour
{
    public GameObject[] objectsAttachtoChain;
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
            Rigidbody chainRB = gameObject.GetComponent<Rigidbody>();
            chainRB.isKinematic = false;
            chainRB.useGravity = true;
        }
        
    }
}
