using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDestroyer : MonoBehaviour
{   
    public GameObject explosionEffectPrefab; // Optional: explosion effect prefab

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RockWall"))
        {
            // Instantiate if there is explosion effect
            if (explosionEffectPrefab != null)
            {
                Instantiate(explosionEffectPrefab, collision.contacts[0].point, Quaternion.identity);
            }

            // Destroy rock
            Destroy(collision.gameObject);

            // Destroy stone
            Destroy(gameObject);
        }
    }

}
