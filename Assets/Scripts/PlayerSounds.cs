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
        WalkSound();
        StopWalkSound();
    }

    void StopWalkSound()
    {
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

    void WalkSound()
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
    }

    public void ChangeSoundToWater()
    {
        movementSounds.clip = stepsWater;
    }

    public void ChangeSoundToGrass()
    {
        movementSounds.clip = stepsGrass;
    }

    public void ChangeEnvironment()
    {
        if(Input.GetKey(KeyCode.W) 
        || Input.GetKey(KeyCode.A)
        || Input.GetKey(KeyCode.S)
        || Input.GetKey(KeyCode.D))
        {
            if(!movementSounds.isPlaying)
            {
                movementSounds.Play();
            }
        }
    }
}
