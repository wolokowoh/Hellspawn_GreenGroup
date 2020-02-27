using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIsHitting : MonoBehaviour
{
    public bool isWeaponHitting;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isWeaponHitting = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isWeaponHitting = false;
        }
    }
}
