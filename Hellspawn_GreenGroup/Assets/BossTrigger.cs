using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public BossHealthBar boss;
    public GameObject Walls;
    private void Start()
    {
        Walls.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            boss.cutOnBar();
            Walls.SetActive(true);
            Destroy(gameObject);
        }
    }
}
