using UnityEngine;

public class FireBowlManager : MonoBehaviour
{
    [Header("Fire & Beacon")]
    public GameObject fire;    // Ateş efekti (örn: Particle System)
    public GameObject beacon;  // Teleport alanı veya ışık işareti

    public GameObject teleportBoat;

    [Header("Path Lights")]
    public GameObject[] lightsOnPath; // Yol üzerindeki ışık objelerini Inspector'dan atayın

    private bool fireLit = false;

    void OnTriggerEnter(Collider other)
    {
        // Make sure your “wood” pieces are tagged “Wood”
        if (!fireLit && other.CompareTag("Wood"))
        {
            fireLit = true;

            // 1) Light the fire
            if (fire != null) fire.SetActive(true);

            // 2) Turn on the beacon
            if (beacon != null)
                beacon.SetActive(true);
            else
                Debug.LogWarning($"[{name}] No beacon assigned in Inspector");

            // 3) Enable all lights on the path
            if (lightsOnPath != null && lightsOnPath.Length > 0)
            {
                foreach (GameObject lightObj in lightsOnPath)
                {
                    if (lightObj != null)
                        lightObj.SetActive(true);
                }
            }

            // 4) Destroy the wood and show your UI hint
            Destroy(other.gameObject);
            GetComponent<FireMessageUIXR>()?.ShowMessage();

            // 5) Teleport the boat
            if (teleportBoat != null)
                teleportBoat.SetActive(true);
            else
                Debug.LogWarning($"[{name}] No teleport boat assigned in Inspector");
        }
    }
}