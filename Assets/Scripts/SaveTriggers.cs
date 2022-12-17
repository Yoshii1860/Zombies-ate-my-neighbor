using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTriggers : MonoBehaviour
{
    [SerializeField] public TriggeredScript[] triggerArray;
    [SerializeField] public string[] triggerStrings;
    int i = 0;

    void Start() 
    {
        Debug.Log("SaveTriggers");
        foreach(TriggeredScript trigger in triggerArray)
        {
            string name = trigger.transform.gameObject.name + i.ToString();
            if (triggerStrings[i] == "null")
            {
                triggerStrings[i] = name;
            }
            i++;
            if(i == triggerArray.Length)
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
            if(trigger.isTriggered && PlayerPrefs.GetString(name) == name)
            {
                triggerStrings[i] = "true";
            }
            i++;
            if(i == triggerArray.Length)
            {
                i = 0;
            }
        }
    }
}
