using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    // Start is called before the first frame update

    [SerializeField] private string[] scenesToLoad;

    void Start()
    {
        // SceneManager.LoadSceneAsync("TentScene", LoadSceneMode.Additive).completed += (operation) => { Debug.Log("TentScene scene loaded successfully.");};

        foreach (string sceneName in scenesToLoad)
        {
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
