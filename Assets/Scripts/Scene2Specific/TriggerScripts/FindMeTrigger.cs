using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMeTrigger : MonoBehaviour
{
    AudioSource audioSource;
    bool triggered = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player" && !triggered)
        {
            audioSource.Play();
            triggered = true;
        }
    }
}
