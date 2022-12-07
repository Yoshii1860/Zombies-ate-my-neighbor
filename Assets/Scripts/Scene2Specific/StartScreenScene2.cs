using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenScene2 : MonoBehaviour
{
    [SerializeField] Image blackScreen;
    [SerializeField] Rigidbody rbPlayer;

    void Start()
    {
        rbPlayer = rbPlayer.GetComponent<Rigidbody>();
        rbPlayer.isKinematic = true;
        Invoke("BlackScreenFadeIn", 14f);
        Invoke("EnableMovement", 14f);

    }

    void EnableMovement()
    {
        rbPlayer.isKinematic = false;
    }

    void BlackScreenFadeIn()
    {
        blackScreen.CrossFadeAlpha(0f, 0f, false);
    }
}
