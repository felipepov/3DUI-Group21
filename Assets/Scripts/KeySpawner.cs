using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    [Tooltip("Drag your KeyInside GameObject here (start it disabled in the scene).")]
    public GameObject keyInside;

    [Tooltip("Drag your TreasureLight GameObject here (start it disabled in the scene).")]
    public GameObject treasureLight;

    [Tooltip("Optional: assign the HMD/Camera Rig’s head transform; if left blank, will use Camera.main.")]
    public Transform playerHead;

    [Tooltip("How close the player needs to get before the key + light spawn.")]
    public float triggerDistance = 4f;

    // Prevents spawning multiple times
    private bool hasSpawned = false;

    void Start()
    {
        // Make sure they start disabled
        if (keyInside != null)      keyInside.SetActive(false);
        if (treasureLight != null)  treasureLight.SetActive(false);

        // Auto‑assign camera if nothing was set
        if (playerHead == null)
        {
            if (Camera.main != null)
                playerHead = Camera.main.transform;
            else
                Debug.LogWarning("KeySpawner: no playerHead assigned and no Camera.main found!");
        }
    }

    void Update()
    {
        // If we've already spawned, nothing more to do
        if (hasSpawned || playerHead == null)
            return;

        // Check distance
        float dist = Vector3.Distance(transform.position, playerHead.position);
        if (dist <= triggerDistance)
        {
            SpawnKeyAndLight();
        }
    }

    private void SpawnKeyAndLight()
    {
        // Activate your prefabs/objects
        if (keyInside != null)     keyInside.SetActive(true);
        if (treasureLight != null) treasureLight.SetActive(true);

        hasSpawned = true;
        Debug.Log("KeySpawner: spawned key and light.");
    }

    // (Optional) draw the trigger radius in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
    }
}
