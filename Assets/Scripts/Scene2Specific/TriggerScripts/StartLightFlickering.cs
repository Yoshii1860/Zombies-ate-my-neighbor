using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class StartLightFlickering : MonoBehaviour
{
    [SerializeField] AddFlickering addFlickeringScript;
    [SerializeField] GameObject scavanger;
    [SerializeField] LightFlickering flashlight;
    [SerializeField] AudioClip flickeringLights;
    [SerializeField] AudioClip girlSound;
    [SerializeField] RigidbodyFirstPersonController rbc;
    AudioSource audioSource;
    float rbcFS = 0f;
    float rbcBS = 0f;
    float rbcSS = 0f;
    float rbcRM = 0f;

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
        GetComponent<TriggeredScript>().isTriggered = true;
        transform.Translate(0,-3,0);
        addFlickeringScript.enabled = true;
        flashlight.enabled = true;
        audioSource.PlayOneShot(flickeringLights, 0.7f);
        audioSource.PlayOneShot(girlSound, 1f);
        WalkSlow();
        StartCoroutine(LightsAndScavanger());
    }

    IEnumerator LightsAndScavanger()
    {
        yield return new WaitForSeconds(7);
            addFlickeringScript.TurnLightsOff();
            flashlight.maxIntensity = 0;
            scavanger.SetActive(true);
        yield return new WaitForSeconds(7);
            ResetWalk();
            flashlight.enabled = false;
            flashlight.gameObject.GetComponent<Light>().intensity = 0.2f;
        yield return new WaitForSeconds(2);
            scavanger.SetActive(false);
    }

    void WalkSlow()
    {
        rbcFS = rbc.movementSettings.ForwardSpeed;
        rbcBS = rbc.movementSettings.BackwardSpeed;
        rbcSS = rbc.movementSettings.StrafeSpeed;
        rbcRM = rbc.movementSettings.RunMultiplier;
        rbc.movementSettings.ForwardSpeed = 4f;
        rbc.movementSettings.BackwardSpeed = 2f;
        rbc.movementSettings.StrafeSpeed = 2f;
        rbc.movementSettings.RunMultiplier = 1f;
    }

    void ResetWalk()
    {
        rbc.movementSettings.ForwardSpeed = rbcFS;
        rbc.movementSettings.BackwardSpeed = rbcBS;
        rbc.movementSettings.StrafeSpeed = rbcSS;
        rbc.movementSettings.RunMultiplier = rbcRM;
    }
}
