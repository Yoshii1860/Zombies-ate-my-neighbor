using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTriggers : MonoBehaviour
{
    [SerializeField] public TriggeredScript[] triggerArray;
    [SerializeField] public string[] triggerStrings;
    int i = 0;

    void OnAwake() 
    {
        Debug.Log("SaveTriggers");
        foreach(TriggeredScript trigger in triggerArray)
        {
            string name = trigger.transform.gameObject.name + i.ToString();
            if(PlayerPrefs.GetString(name) == "true")
            {
                return;
            }
            else
            {
                triggerStrings[i] = name;
            }

            if(i != triggerArray.Length-1)
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
        foreach(TriggeredScript trigger in triggerArray)
        {
            string name = trigger.transform.gameObject.name + i.ToString();
            if(PlayerPrefs.GetString(name) == "true")
            {
                return;
            }
            else if(trigger.isTriggered)
            {
                triggerStrings[i] = "true";
            }

            if(i != triggerArray.Length-1)
            {
                i++;
                Debug.Log(i);
            }
            else
            {
                i = 0;
                Debug.Log(i);
            }
        }
    }
}
