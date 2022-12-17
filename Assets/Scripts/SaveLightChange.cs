using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLightChange : MonoBehaviour
{
    [SerializeField] GameObject asylumLights;
    [SerializeField] GameObject newAsylumLights;
    SaveTriggers triggers;
    Light[] lights;
    Light[] newLights;

    void Start()
    {
        triggers = GetComponent<SaveTriggers>();
        lights = asylumLights.GetComponentsInChildren<Light>();
        newLights = newAsylumLights.GetComponentsInChildren<Light>();

        foreach(Light light in newLights)
        {
            LightFlickering lightComp = light.gameObject.GetComponent<LightFlickering>();
            lightComp.enabled = true;
        }
        foreach(Light light in lights)
        {
            light.gameObject.SetActive(false);
        }
    }
}
