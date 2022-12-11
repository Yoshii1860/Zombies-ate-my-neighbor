using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DentistDollTrigger : MonoBehaviour
{
    [SerializeField] GameObject ghoul;
    AudioSource audioSource;
    public bool triggered = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        ghoul.SetActive(false);
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player" && !triggered)
        {
            audioSource.Play();
            triggered = true;
            ghoul.SetActive(true);
        }
    }
}