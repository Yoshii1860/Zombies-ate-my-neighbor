using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenScene2 : MonoBehaviour
{
    [SerializeField] Image blackScreen;
    [SerializeField] Rigidbody rbPlayer;
    [SerializeField] AudioClip startAudio;
    [SerializeField] AudioClip gaspAudio;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rbPlayer = rbPlayer.GetComponent<Rigidbody>();
        rbPlayer.isKinematic = true;
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        audioSource.PlayOneShot(startAudio, 1f);
        yield return new WaitForSeconds(14);
            rbPlayer.isKinematic = false;
            while(audioSource.isPlaying)
            {
                yield return null;
            }
            audioSource.PlayOneShot(gaspAudio, 1f);
            blackScreen.CrossFadeAlpha(0f, 0f, false);
    }
}
