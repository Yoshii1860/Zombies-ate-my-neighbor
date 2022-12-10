using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class RifleTrigger : MonoBehaviour
{
    [SerializeField] AudioClip girlLaughing;
    [SerializeField] Weapon rifle;
    [SerializeField] RigidbodyFirstPersonController rbc;
    AudioSource audioSource;
    int child;
    bool justOnce = false;
    float rbcFS = 0f;
    float rbcBS = 0f;
    float rbcSS = 0f;
    float rbcRM = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        child = transform.childCount;
        if(child == 2 && rifle.pickedUp && !justOnce)
        {
            WalkSlow();
            justOnce = true;
            audioSource.PlayOneShot(girlLaughing, 1f);
            Invoke("ResetWalk", 4f);
        }
    }

    void WalkSlow()
    {
        rbcFS = rbc.movementSettings.ForwardSpeed;
        rbcBS = rbc.movementSettings.BackwardSpeed;
        rbcSS = rbc.movementSettings.StrafeSpeed;
        rbcRM = rbc.movementSettings.RunMultiplier;
        rbc.movementSettings.ForwardSpeed = 4f;
        rbc.movementSettings.BackwardSpeed = 2f;
        rbc.movementSettings.StrafeSpeed = 2f;
        rbc.movementSettings.RunMultiplier = 1f;
    }

    void ResetWalk()
    {
        rbc.movementSettings.ForwardSpeed = rbcFS;
        rbc.movementSettings.BackwardSpeed = rbcBS;
        rbc.movementSettings.StrafeSpeed = rbcSS;
        rbc.movementSettings.RunMultiplier = rbcRM;
    }
}
