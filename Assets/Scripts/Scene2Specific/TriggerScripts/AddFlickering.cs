using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFlickering : MonoBehaviour
{
    [SerializeField] GameObject asylumLights;
    GameObject asylum;
    Light[] lights;
    Light[] newLights;
    
    void Awake() 
    {
        newLights = asylumLights.GetComponentsInChildren<Light>();
        foreach (Light light in newLights)
        {
            light.gameObject.GetComponent<LightFlickering>().enabled = false;
        }
    }

    void Start()
    {
        asylum = transform.gameObject;
        lights = asylum.GetComponentsInChildren<Light>();

        foreach(Light light in lights)
        {
            if(light.gameObject.activeSelf)
            {
                light.gameObject.AddComponent<LightFlickering>();
            }
        }
    }

    public void TurnLightsOff()
    {
        foreach(Light light in lights)
        {
            LightFlickering lightComp = light.gameObject.GetComponent<LightFlickering>();
            lightComp.maxIntensity = 0f;
        }
        foreach(Light light in newLights)
        {
            StartCoroutine(ActivateNewLight(light));
        }
    }

    IEnumerator ActivateNewLight(Light light)
    {
        yield return new WaitForSeconds(5);
        light.gameObject.GetComponent<LightFlickering>().enabled = true;
    }
}
