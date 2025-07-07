using UnityEngine;
using UnityEditor;

public static class SnapToTerrain
{
    [MenuItem("Tools/Snap Selected To Terrain")]
    private static void SnapSelectedToTerrain()
    {
        Terrain terrain = Terrain.activeTerrain;
        if (terrain == null)
        {
            Debug.LogError("No active Terrain found!");
            return;
        }

        foreach (GameObject obj in Selection.gameObjects)
        {
            Vector3 pos = obj.transform.position;
            float terrainHeight = terrain.SampleHeight(pos) + terrain.GetPosition().y;
            obj.transform.position = new Vector3(pos.x, terrainHeight, pos.z);
        }
        Debug.Log("Snapped selected objects to terrain height.");
    }
}
