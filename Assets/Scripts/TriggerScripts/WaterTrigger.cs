using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    [SerializeField] PlayerSounds playerSounds;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            playerSounds.ChangeSoundToWater();
            playerSounds.ChangeEnvironment();
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            playerSounds.ChangeSoundToGrass();
            playerSounds.ChangeEnvironment();
        }
    }
}
