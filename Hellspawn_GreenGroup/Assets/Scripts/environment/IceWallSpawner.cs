using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWallSpawner : MonoBehaviour
{
    public GameObject spawnedFloor;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnedFloor.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            if (!spawnedFloor.activeInHierarchy)
            {
                spawnedFloor.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
