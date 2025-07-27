using UnityEngine;  // ← needed for MonoBehaviour, Collider, GameObject, [Header]
 
public class FireBowlManager : MonoBehaviour
{
    [Header("Fire & Beacon")]
    public GameObject fire;    // your fire‑VFX or flame object
    public GameObject beacon;  // drag your disabled Beacon GameObject here

    void OnTriggerEnter(Collider other)
    {
        // Make sure your “wood” pieces are tagged “Wood”
        if (other.CompareTag("Wood"))
        {
            // 1) light the fire
            fire.SetActive(true);

            // 2) turn on the beacon
            if (beacon != null)
                beacon.SetActive(true);
            else
                Debug.LogWarning($"[{name}] No beacon assigned in Inspector");

            // 3) destroy the wood and show your UI hint
            Destroy(other.gameObject);
            GetComponent<FireMessageUIXR>()?.ShowMessage();
        }
    }
}
