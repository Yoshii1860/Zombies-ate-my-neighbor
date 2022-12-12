using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeTrigger : MonoBehaviour
{
    [SerializeField] GameObject horde;
    [SerializeField] AudioSource environment;
    [SerializeField] AudioClip chase;
    AudioSource audioSource;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(StartFadeOut());
            audioSource.Play();
            StartCoroutine(HordeActive());
        }
    }

    IEnumerator HordeActive()
    {
        yield return new WaitWhile (() => audioSource.isPlaying);
        audioSource.PlayOneShot(chase, 1f);
        yield return new WaitForSeconds(3);
        horde.SetActive(true);
        yield return new WaitWhile (() => audioSource.isPlaying);
        StartCoroutine(StartFadeIn());
    }

    IEnumerator StartFadeOut()
    {
        float currentTime = 0;
        float start = environment.volume;
        float duration = 2;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            environment.volume = Mathf.Lerp(start, 0, currentTime / duration);
            yield return null;
        }
    }

    IEnumerator StartFadeIn()
    {
        float currentTime = 0;
        float start = 0;
        float duration = 2;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            environment.volume = Mathf.Lerp(start, 1, currentTime / duration);
            yield return null;
        }
    }
}
