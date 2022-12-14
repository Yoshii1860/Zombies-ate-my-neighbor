using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    [SerializeField] Image blackScreen;
    [SerializeField] Rigidbody rbPlayer;
    AudioSource playerAudio;

    void Start()
    {
        rbPlayer = rbPlayer.GetComponent<Rigidbody>();
        rbPlayer.isKinematic = true;
        playerAudio = rbPlayer.GetComponent<AudioSource>();
        playerAudio.enabled = false;
        Invoke("EnableAudio", 6f);
        Invoke("BlackScreenFadeIn", 1.5f);
        Invoke("EnableMovement", 6f);

    }

    void EnableAudio()
    {
        playerAudio.enabled = true;
    }

    void EnableMovement()
    {
        rbPlayer.isKinematic = false;
    }

    void BlackScreenFadeIn()
    {
        blackScreen.CrossFadeAlpha(0f, 6f, false);
    }
}
