using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollTrigger : MonoBehaviour
{
    [SerializeField] Weapon rifle;
    [SerializeField] GameObject doll;
    [SerializeField] LightFlickering light;
    [SerializeField] GameObject flashlight;
    [SerializeField] GameObject blood;
    [SerializeField] GameObject zombies;
    [SerializeField] AudioClip lightSound;
    [SerializeField] AudioClip footStepSound;
    [SerializeField] AudioClip girlBehindSound;
    [SerializeField] GameObject moveDeadBody;
    [SerializeField] FogRenderer fogRenderer;
    AudioSource audioSource;

    bool isInside = false;
    int count = 0;
    public bool dollTrigger = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        blood.SetActive(false);
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(!isInside)
            {
                transform.Translate(3.13f, 0, 0);
                isInside = true;
                audioSource.PlayOneShot(footStepSound, 0.7f);
            }
            count ++;
        }

        if(count >= 2 && rifle.pickedUp && !dollTrigger)
        {
            dollTrigger = true;
            audioSource.PlayOneShot(girlBehindSound, 1f);
            light.maxIntensity = 0f;
            flashlight.SetActive(false);
            fogRenderer.enabled = false;
            RenderSettings.fogEndDistance = 1f;
            moveDeadBody.transform.position = new Vector3(623.42f,3.107f,196.199f);
            audioSource.PlayOneShot(lightSound, 0.4f);
            StartCoroutine(NewTriggerEvent());
        }
    }

    IEnumerator NewTriggerEvent()
    {
        yield return new WaitForSeconds(2);
            blood.SetActive(true);
            zombies.SetActive(true);
            doll.transform.Translate(-6.14f, -1.38f, 0);
            RenderSettings.fogEndDistance = 10f;
            fogRenderer.enabled = true;
            flashlight.SetActive(true);
            light.maxIntensity = 0.2f;
            light.smoothing = 15;
            light.gameObject.GetComponent<Light>().color = new Color(1,0,0,0.8f);
            transform.parent.GetComponent<TriggeredScript>().isTriggered = true;
    }

    public void publicTriggerEvent()
    {
        blood.SetActive(true);
        doll.transform.Translate(-6.14f, -1.38f, 0);
        light.smoothing = 15;
        light.gameObject.GetComponent<Light>().color = new Color(1,0,0,0.8f);
        transform.parent.GetComponent<TriggeredScript>().isTriggered = true;
    }
}
