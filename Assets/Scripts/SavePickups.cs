using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePickups : MonoBehaviour
{   
    [SerializeField] public GameObject[] pickupObjects;
    [SerializeField] public string[] pickupStrings;
    int i = 0;

    void Start() 
    {
        Debug.Log("SavePickups");
        foreach(GameObject pickup in pickupObjects)
        {
            string name = pickup.name + i.ToString();
            if(pickupStrings[i] == "null")
            {
                pickupStrings[i] = name;
            }
            i++;
            if(i == pickupObjects.Length)
            {
                i = 0;
            }
        }
    }

    void Update() 
    {
        foreach(GameObject pickup in pickupObjects)
        {
            string name = pickup.name + i.ToString();  
            if(!pickup.activeSelf && PlayerPrefs.GetString(name) == name)
            {
                    pickupStrings[i] = "true";
            }
            i++;
            if(i == pickupStrings.Length)
            {
                i = 0;
            }
        }
    }
}
