using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    [Tooltip("Drag your KeyInside GameObject here (start it disabled in the scene).")]
    public GameObject keyInside;
    public GameObject treasureLight;


    // Prevents spawning multiple times
    private bool hasSpawned = false;

    void Start()
    {
        // Ensure it starts off
        if (keyInside != null)
            keyInside.SetActive(false);

        if (treasureLight != null)
            treasureLight.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // Only trigger once, and only when the Player enters
        if (!hasSpawned && other.CompareTag("Player"))
        {
            keyInside.SetActive(true);
            treasureLight.SetActive(true);
            hasSpawned = true;
        }
    }
}
