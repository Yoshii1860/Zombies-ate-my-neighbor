using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    [SerializeField] GameObject playerSounds;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            playerSounds.GetComponent<PlayerSounds>().ChangeSoundToWater();
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            playerSounds.GetComponent<PlayerSounds>().ChangeSoundToGrass();
        }
    }
}
