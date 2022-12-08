using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleTrigger : MonoBehaviour
{
    [SerializeField] AudioClip girlLaughing;
    [SerializeField] Weapon rifle;
    AudioSource audioSource;
    int child;
    bool justOnce = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        child = transform.childCount;
        if(child == 2 && rifle.pickedUp && !justOnce)
        {
            justOnce = true;
            audioSource.PlayOneShot(girlLaughing, 1f);
        }
    }
}
