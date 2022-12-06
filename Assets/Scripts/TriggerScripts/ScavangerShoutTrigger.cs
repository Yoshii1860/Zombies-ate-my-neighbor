using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavangerShoutTrigger : MonoBehaviour
{
    [SerializeField] AudioSource scavangerScream;
    [SerializeField] GameObject scavanger;

    void Start() {
        scavanger.SetActive(false);
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            scavangerScream.Play();
            scavanger.SetActive(true);
            Destroy(scavanger, 15f);
            Destroy(gameObject);
        }
    }
}
