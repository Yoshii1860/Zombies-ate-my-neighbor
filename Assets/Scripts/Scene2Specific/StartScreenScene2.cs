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
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        yield return new WaitForSeconds(14);
            rbPlayer.isKinematic = false;
            blackScreen.CrossFadeAlpha(0f, 0f, false);
    }
}
