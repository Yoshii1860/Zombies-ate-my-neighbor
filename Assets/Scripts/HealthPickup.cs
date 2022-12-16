using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] AudioSource pickup;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerHealth>().ChangeHealth();
            pickup.Play();
            gameObject.SetActive(false);
        }
    }
}
