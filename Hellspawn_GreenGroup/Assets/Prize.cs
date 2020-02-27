using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
    public GameObject prize;
    public Vector3 positionToSpawn;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            Instantiate(prize, positionToSpawn, prize.transform.rotation);
            Destroy(gameObject);
        }
    }
}
