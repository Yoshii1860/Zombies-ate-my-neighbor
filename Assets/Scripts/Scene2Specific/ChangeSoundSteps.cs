using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSoundSteps : MonoBehaviour
{
    [SerializeField] PlayerSounds playerSounds;
    bool isInside = false;

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(!isInside)
            {
                playerSounds.ChangeSoundToInside();
                playerSounds.ChangeEnvironment();
                isInside = true;
            }
            else
            {
                playerSounds.ChangeSoundToGrass();
                playerSounds.ChangeEnvironment();
                isInside = false;
            }
        }
    }
}
