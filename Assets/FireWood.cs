using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWood : MonoBehaviour
{
    public GameObject fire;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            ActivateFire();
        }
    }

    void ActivateFire()
    {
        fire.SetActive(true);
    }
}
