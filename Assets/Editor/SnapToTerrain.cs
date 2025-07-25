using UnityEngine;
using UnityEditor;

public static class SnapToTerrain
{
    [MenuItem("Tools/Snap Selected To Closest Terrain")]
    private static void SnapSelectedToTerrain()
    {
        Terrain[] terrains = Terrain.activeTerrains;
        if (terrains.Length == 0)
        {
            Debug.LogError("No active Terrains found in the scene!");
            return;
        }

        foreach (GameObject obj in Selection.gameObjects)
        {
            Terrain closestTerrain = GetTerrainUnderPosition(obj.transform.position, terrains);
            if (closestTerrain == null)
            {
                Debug.LogWarning($"No terrain found under object: {obj.name} at {obj.transform.position}");
                continue;
            }

            Vector3 worldPos = obj.transform.position;
            Vector3 terrainOrigin = closestTerrain.GetPosition();

            // Correct height sampling
            float terrainHeight = closestTerrain.SampleHeight(worldPos) + terrainOrigin.y;

            obj.transform.position = new Vector3(worldPos.x, terrainHeight, worldPos.z);
        }

        Debug.Log("Snapped selected objects to terrain height.");
    }

    private static Terrain GetTerrainUnderPosition(Vector3 position, Terrain[] terrains)
    {
        foreach (Terrain t in terrains)
        {
            Vector3 terrainPos = t.GetPosition();
            Vector3 terrainSize = t.terrainData.size;

            Bounds bounds = new Bounds(
                terrainPos + terrainSize * 0.5f,
                terrainSize
            );

            if (bounds.Contains(position))
            {
                return t;
            }
        }

        return null;
    }
}
