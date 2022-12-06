using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleZombieTrigger : MonoBehaviour
{
    [SerializeField] GameObject zombie;
    [SerializeField] AudioSource audio;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            zombie.SetActive(true);
            audio.Play();
            Destroy(gameObject);
        }
    }
}
