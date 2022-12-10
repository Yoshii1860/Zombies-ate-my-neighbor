using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSoundSteps : MonoBehaviour
{
    [SerializeField] PlayerSounds playerSounds;
    [SerializeField] bool outside = false;

    void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(outside)
            { 
                playerSounds.ChangeSoundToInside();
                playerSounds.ChangeEnvironment();
            }
            else
            { 
                playerSounds.ChangeSoundToGrass();
                playerSounds.ChangeEnvironment();
            }
        }
    }
}
