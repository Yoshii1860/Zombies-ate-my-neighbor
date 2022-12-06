using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AudioSource movementSounds;
    [SerializeField] AudioClip stepsGrass;
    [SerializeField] AudioClip stepsWater;

    private void Start() 
    {
        movementSounds.clip = stepsGrass;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) 
        || Input.GetKeyDown(KeyCode.A)
        || Input.GetKeyDown(KeyCode.S)
        || Input.GetKeyDown(KeyCode.D))
        {
            if(!movementSounds.isPlaying)
            {
                movementSounds.Play();
            }
        }

        if(Input.GetKeyUp(KeyCode.W) 
        || Input.GetKeyUp(KeyCode.A)
        || Input.GetKeyUp(KeyCode.S)
        || Input.GetKeyUp(KeyCode.D))
        {
            Invoke("StopAudio", 0.5f);
        }
    }

    void StopAudio()
    {
        if (Input.GetKey(KeyCode.W) == false
        && Input.GetKey(KeyCode.A) == false
        && Input.GetKey(KeyCode.S) == false
        && Input.GetKey(KeyCode.D) == false)
        {
            movementSounds.Stop();
        }
    }

    public void ChangeSoundToWater()
    {
        movementSounds.clip = stepsWater;
    }

    public void ChangeSoundToGrass()
    {
        movementSounds.clip = stepsGrass;
    }
}
