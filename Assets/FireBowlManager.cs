using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBowlManager : MonoBehaviour
{
    public GameObject fire;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            ActivateFire();
            Destroy(other.gameObject);
        }
    }

    void ActivateFire()
    {
        fire.SetActive(true);
    }
}
