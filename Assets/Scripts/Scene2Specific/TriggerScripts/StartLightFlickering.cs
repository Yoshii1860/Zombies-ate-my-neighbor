using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLightFlickering : MonoBehaviour
{
    [SerializeField] AddFlickering addFlickeringScript;
    [SerializeField] GameObject scavanger;
    [SerializeField] LightFlickering flashlight;
    [SerializeField] AudioClip flickeringLights;
    [SerializeField] AudioClip girlSound;
    AudioSource audioSource;

    void Awake() 
    {
        addFlickeringScript.enabled = false;
        flashlight.enabled = false;
    }

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) 
    {
        transform.Translate(0,-3,0);
        addFlickeringScript.enabled = true;
        flashlight.enabled = true;
        audioSource.PlayOneShot(flickeringLights, 0.7f);
        audioSource.PlayOneShot(girlSound, 1f);
        StartCoroutine(LightsAndScavanger());
    }

    IEnumerator LightsAndScavanger()
    {
        yield return new WaitForSeconds(7);
            addFlickeringScript.TurnLightsOff();
            flashlight.maxIntensity = 0;
            scavanger.SetActive(true);
        yield return new WaitForSeconds(7);
            flashlight.enabled = false;
            flashlight.gameObject.GetComponent<Light>().intensity = 0.2f;
        yield return new WaitForSeconds(2);
            scavanger.SetActive(false);
    }
}
