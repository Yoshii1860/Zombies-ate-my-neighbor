using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AudioSource movementSounds;
    [SerializeField] AudioClip stepsGrass;
    [SerializeField] AudioClip stepsWater;
    [SerializeField] AudioClip stepsInside;
    [SerializeField] AudioClip jumpStart;
    [SerializeField] AudioClip jumpEnd;
    [SerializeField] AudioSource jumpSounds;
    RigidbodyFirstPersonController rbc;
    bool oneShot = true;

    private void Start() 
    {
        movementSounds.clip = stepsGrass;
        rbc = GetComponent<RigidbodyFirstPersonController>();
    }

    void Update()
    {
        WalkSound();
        StopWalkSound();
        JumpSound();
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

    void JumpSound()
    {
        if(rbc.Jumping && oneShot)
        {
            oneShot = false;
            StopSounds();
            jumpSounds.PlayOneShot(jumpStart, 1f);
            StartCoroutine(JumpSounds());
            StartSounds();
        }
        else if (!rbc.Jumping && !oneShot)
        {
            oneShot = true;
            ChangeEnvironment();
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

    public void ChangeSoundToInside()
    {
        movementSounds.clip = stepsInside;
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

    public void StopSounds()
    {
        movementSounds.enabled = false;
    }

    public void StartSounds()
    {
        movementSounds.enabled = true;
    }

    IEnumerator JumpSounds()
    {
        yield return new WaitWhile (() => rbc.Jumping);
        jumpSounds.PlayOneShot(jumpEnd, 1f);
    }
}
