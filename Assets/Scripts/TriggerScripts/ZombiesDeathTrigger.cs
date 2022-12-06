using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiesDeathTrigger : MonoBehaviour
{
    [SerializeField] AudioSource horde;
    [SerializeField] AudioSource ghoul;
    [SerializeField] AudioSource slash1;
    [SerializeField] AudioSource slash2;
    [SerializeField] AudioSource slash3;
    [SerializeField] AudioSource walk;

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            horde.Play(); //12 secs -- 12 secs
            walk.PlayDelayed(4f); //12 secs -- 14 secs
            ghoul.PlayDelayed(6f); //5 secs -- 9 secs
            slash1.PlayDelayed(8f); //1 sec -- 7 secs
            slash2.PlayDelayed(10f); //1 sec -- 9 secs
            slash2.PlayDelayed(12f); //1 sec -- 11secs
        }
        Destroy(gameObject);
    }
}
