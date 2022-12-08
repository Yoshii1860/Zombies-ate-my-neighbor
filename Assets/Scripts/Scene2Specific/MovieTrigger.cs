using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MovieTrigger : MonoBehaviour
{
    [SerializeField] AudioClip projectorStart;
    [SerializeField] AudioClip projectorStop;
    [SerializeField] GameObject videoScreen;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject projectorLight;
    [SerializeField] AudioSource girlSound;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject scavangerPool;
    AudioSource audioSource;
    bool activeVideo = false;

    void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
        projectorLight.SetActive(false);
        videoScreen.SetActive(false);
        videoPlayer.enabled = false;
        scavangerPool.SetActive(false);
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            rb.isKinematic = true;
            transform.GetComponent<BoxCollider>().enabled = false;
            projectorLight.SetActive(true);
            audioSource.PlayOneShot(projectorStart, 0.2f);
            videoScreen.SetActive(true);
            videoPlayer.enabled = true;
            StartCoroutine(ActiveSequence());
        }
    }
    IEnumerator ActiveSequence()
    {
        yield return new WaitForSeconds(2);
            yield return new WaitWhile (() => videoPlayer.isPlaying);
                rb.isKinematic = false;
                audioSource.Stop();
                audioSource.PlayOneShot(projectorStop, 0.2f);
                projectorLight.SetActive(false);
                videoScreen.SetActive(false);
                yield return new WaitForSeconds(5);
                    girlSound.Play();
                        yield return new WaitWhile (() => girlSound.isPlaying);
                            yield return new WaitForSeconds(1);
                                scavangerPool.SetActive(true);
            }
}
