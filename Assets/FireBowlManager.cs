using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBowlManager : MonoBehaviour
{
    public ParticleSystem fireEffect;
    public int woodCount = 0;
    public int requiredWood = 3;

    public GameObject[] teleportPoints;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            Destroy(other.gameObject); 
            woodCount++;
            IncreaseFire();

            if (woodCount >= requiredWood)
            {
                ActivateTeleportPoints();
            }
        }
    }

    void IncreaseFire()
    {
        var main = fireEffect.main;
        main.startSize = Mathf.Clamp(main.startSize.constant + 0.5f, 1f, 4f);
    }

    void ActivateTeleportPoints()
    {
        foreach (GameObject tp in teleportPoints)
        {
            tp.SetActive(true);
            tp.GetComponent<ParticleSystem>()?.Play(); 
        }
    }
}
