using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStairs : MonoBehaviour
{
    [SerializeField] GameObject barrickade;
    [SerializeField] GameObject barrickadeNew;
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;
    [SerializeField] AudioClip clip3;
    [SerializeField] AudioClip clip4;
    [SerializeField] AudioClip clip5;
    [SerializeField] AudioSource audioSource;

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            barrickade.SetActive(false);
            barrickadeNew.SetActive(true);
            StartCoroutine(MakeMusicPlay());
        }
    }

    IEnumerator MakeMusicPlay()
    {
        audioSource.PlayOneShot(clip2, 1f);
        yield return new WaitForSeconds(1);
        audioSource.PlayOneShot(clip5, 1f);
        yield return new WaitForSeconds(1);
        audioSource.PlayOneShot(clip1, 1f);
        audioSource.PlayOneShot(clip3, 1f);
        yield return new WaitForSeconds(1);
        audioSource.PlayOneShot(clip4, 1f);
        yield return null;
    }
}
