using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private KeyDestroyer key;
    public GameObject keyText;
    private ShovelCraftingManager shovel;
    public GameObject shovelText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(key.pickedUp == true)
        {
            keyText.SetActive(true);
        }

        if (shovel.isCrafted == true) 
        { 
            shovelText.SetActive(true) ;
        }
    }
}
