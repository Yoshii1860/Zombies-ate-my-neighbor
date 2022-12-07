using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScavangerEnter : MonoBehaviour
{
    [SerializeField] GameObject[] scavangerAll;
    [SerializeField] Image blackScreen;
    [SerializeField] float durationFadeOutAudio = 4f;
    [SerializeField] AudioSource slashAudio;
    [SerializeField] AudioSource heartbeatAudio;
    [SerializeField] SceneLoader gameSession;
    [SerializeField] AudioSource environmentMusic;

    AudioSource fadeAudioSource;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            foreach(GameObject scavanger in scavangerAll)
            {
                if(!scavanger.activeSelf)
                {
                    scavanger.SetActive(true);
                }
                scavanger.GetComponent<EnemyAI>().chaseRange = 500f;
                Invoke("BlackScreenFadeIn", 8f);
                Invoke("FadeOutMusic", 8f);
                Invoke("StopScavangers", 10f);
                heartbeatAudio.PlayDelayed(12f);
                Invoke("NextSceneForDelay", 22f);
            }
        }
    }

    void FadeOutMusic()
    {
        StartCoroutine(StartFade(environmentMusic, durationFadeOutAudio, 0f));
    }

    void BlackScreenFadeIn()
    {
        blackScreen.CrossFadeAlpha(1f, 4f, false);
    }

    void StopScavangers()
    {
        foreach(GameObject scavanger in scavangerAll)
        {
            scavanger.GetComponent<EnemyAttack>().enabled = false;
            fadeAudioSource = scavanger.GetComponent<AudioSource>();
            StartCoroutine(StartFade(fadeAudioSource, durationFadeOutAudio, 0f));
        }
        StartCoroutine(StartFade(slashAudio, durationFadeOutAudio, 0f));
    }

    IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    void NextSceneForDelay()
    {
        gameSession.NextScene();
    }
}
