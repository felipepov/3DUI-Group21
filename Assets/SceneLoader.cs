using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadSceneAsync("TentScene", LoadSceneMode.Additive).completed += (operation) =>
        {
            Debug.Log("TentScene scene loaded successfully.");
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
