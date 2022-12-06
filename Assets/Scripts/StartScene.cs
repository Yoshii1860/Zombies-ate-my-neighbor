using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    [SerializeField] Image blackScreen;
    [SerializeField] Rigidbody rbPlayer;

    void Start()
    {
        rbPlayer = rbPlayer.GetComponent<Rigidbody>();
        rbPlayer.isKinematic = true;
        Invoke("BlackScreenFadeIn", 1.5f);
        Invoke("EnableMovement", 6f);

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
