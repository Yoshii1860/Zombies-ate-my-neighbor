using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Scene2End : MonoBehaviour
{
    [SerializeField] GameObject girl;
    [SerializeField] GameObject flashlight;
    [SerializeField] AudioClip blackout;
    [SerializeField] AudioClip girlSound;
    [SerializeField] AudioSource endMusic;
    [SerializeField] RigidbodyFirstPersonController rbc;
    [SerializeField] GameObject[] lights;
    [SerializeField] AudioSource environment;
    [SerializeField] Image endScreen;
    [SerializeField] Image endText;
    [SerializeField] GameObject reticle;
    [SerializeField] GameObject weapons;
    [SerializeField] PlayerSounds playerSounds;
    [SerializeField] GameObject quit;
    AudioSource audioSource;
    LoadGame loadGame;
    float rbcFS = 0f;
    float rbcBS = 0f;
    float rbcSS = 0f;
    float rbcRM = 0f;
    float rbcMSX = 0f;
    float rbcMSY = 0f;
    bool once = false;

    void Awake() 
    {
        loadGame = FindObjectOfType<LoadGame>();
        loadGame.enabled = true;
    }

    void Start() 
    {
        endScreen.CrossFadeAlpha(0f, 0f, false);
        endText.CrossFadeAlpha(0f, 0f, false);
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) 
    {
        if(!once)
        StartCoroutine(LightsAndGirl());
    }

    IEnumerator LightsAndGirl()
    {
        WalkSlow();
        once = true;
        audioSource.PlayOneShot(blackout, 0.7f);
        StartCoroutine(StartFadeOut());
        yield return new WaitForSeconds(1);
        yield return new WaitWhile (() => audioSource.isPlaying);
        foreach(GameObject light in lights)
        {
            light.SetActive(false);
        }        
        flashlight.SetActive(false);
        RenderSettings.fogColor = new Color(0,0,0,1);
        RenderSettings.fogEndDistance = 15f;
        girl.SetActive(true);
        yield return new WaitForSeconds(1);
        audioSource.PlayOneShot(girlSound, 1);
        yield return new WaitForSeconds(1);
        yield return new WaitWhile (() => audioSource.isPlaying);
        endMusic.Play();
        foreach(GameObject light in lights)
        {
            light.SetActive(true);
            light.GetComponent<LightFlickering>().enabled = true;
        }   
        yield return new WaitForSeconds(2);
        endScreen.CrossFadeAlpha(1f, 4f, false);
        yield return new WaitForSeconds(2);
        rbc.enabled = false;
        reticle.SetActive(false);
        weapons.SetActive(false);
        playerSounds.enabled = false;
        endText.CrossFadeAlpha(1f, 2f, false);
        yield return new WaitForSeconds(5);
        quit.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void WalkSlow()
    {
        rbcFS = rbc.movementSettings.ForwardSpeed;
        rbcBS = rbc.movementSettings.BackwardSpeed;
        rbcSS = rbc.movementSettings.StrafeSpeed;
        rbcRM = rbc.movementSettings.RunMultiplier;
        rbcMSX = rbc.mouseLook.XSensitivity;
        rbcMSY = rbc.mouseLook.YSensitivity;
        rbc.movementSettings.ForwardSpeed = 4f;
        rbc.movementSettings.BackwardSpeed = 2f;
        rbc.movementSettings.StrafeSpeed = 2f;
        rbc.movementSettings.RunMultiplier = 1f;
        rbc.mouseLook.XSensitivity = 0.5f;
        rbc.mouseLook.YSensitivity = 0.5f;
    }

    IEnumerator StartFadeOut()
    {
        float currentTime = 0;
        float start = environment.volume;
        float duration = 3;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            environment.volume = Mathf.Lerp(start, 0, currentTime / duration);
            yield return null;
        }
    }
}
