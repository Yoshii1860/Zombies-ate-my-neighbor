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
    [SerializeField] AudioClip lightSound;
    [SerializeField] AudioClip footStepSound;
    [SerializeField] AudioClip girlBehindSound;
    AudioSource audioSource;

    bool isInside = false;
    bool notFinished = false;
    int count = 0;

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

        if(count >= 2 && rifle.pickedUp && !notFinished)
        {
            audioSource.PlayOneShot(girlBehindSound, 1f);
            notFinished = true;
            light.maxIntensity = 0f;
            flashlight.SetActive(false);
            audioSource.PlayOneShot(lightSound, 0.4f);
            StartCoroutine(NewTriggerEvent());
        }
    }

    IEnumerator NewTriggerEvent()
    {
        yield return new WaitForSeconds(2);
            blood.SetActive(true);
            doll.transform.Translate(-6.14f, -1.38f, 0);
            flashlight.SetActive(true);
            light.maxIntensity = 0.2f;
            light.smoothing = 15;
            light.gameObject.GetComponent<Light>().color = new Color(1,0,0,0.8f);
    }

}
