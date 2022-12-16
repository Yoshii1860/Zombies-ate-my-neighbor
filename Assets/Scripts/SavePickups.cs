using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePickups : MonoBehaviour
{   
    [SerializeField] public GameObject[] pickupObjects;
    [SerializeField] public string[] pickupStrings;
    int i = 0;

    void OnAwake() 
    {
        Debug.Log("SavePickups");
        foreach(GameObject pickup in pickupObjects)
        {
            string name = pickup.name + i.ToString();
            if(PlayerPrefs.GetString(name) == "true")
            {
                return;
            }
            else
            {
                pickupStrings[i] = name;
            }

            if(i != pickupObjects.Length-1)
            {
                i++;
            }
            else
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
            if(PlayerPrefs.GetString(name) == "true")
            {
                return;
            }
            else if(!pickup.activeSelf)
            {
                    pickupStrings[i] = "true";
            }

            if(i != pickupObjects.Length-1)
            {
                i++;
            }
            else
            {
                i = 0;
            }
        }
    }
}
