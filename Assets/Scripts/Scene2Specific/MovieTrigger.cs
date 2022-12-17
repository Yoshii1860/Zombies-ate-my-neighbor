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
    [SerializeField] GameObject savePoint;
    PlayerSounds ps;
    AudioSource audioSource;
    bool activeVideo = false;
    public bool movieTrigger = false;

    void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
        projectorLight.SetActive(false);
        videoScreen.SetActive(false);
        videoPlayer.enabled = false;
    }

    private void Start() 
    {
        ps = rb.gameObject.GetComponent<PlayerSounds>();
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            movieTrigger = true;
            rb.isKinematic = true;
            transform.GetComponent<BoxCollider>().enabled = false;
            projectorLight.SetActive(true);
            audioSource.PlayOneShot(projectorStart, 0.2f);
            videoScreen.SetActive(true);
            videoPlayer.enabled = true;
            ps.StopSounds();
            StartCoroutine(ActiveSequence());
        }
    }
    IEnumerator ActiveSequence()
    {
        yield return new WaitForSeconds(2);
            yield return new WaitWhile (() => videoPlayer.isPlaying);
                rb.isKinematic = false;
                savePoint.SetActive(true);
                ps.StartSounds();
                audioSource.Stop();
                audioSource.PlayOneShot(projectorStop, 0.2f);
                projectorLight.SetActive(false);
                videoScreen.SetActive(false);
                yield return new WaitForSeconds(5);
                    girlSound.Play();
                        yield return new WaitWhile (() => girlSound.isPlaying);
                            yield return new WaitForSeconds(1);
                                scavangerPool.SetActive(true);
                                transform.parent.GetComponent<TriggeredScript>().isTriggered = true;
            }
}
