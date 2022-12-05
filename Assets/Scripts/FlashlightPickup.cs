using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
    [SerializeField] GameObject flashlight;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            flashlight.SetActive(true);
            Destroy(gameObject);
        }
    }
}
