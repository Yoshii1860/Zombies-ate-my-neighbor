using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class StartScreenScene2 : MonoBehaviour
{
    [SerializeField] Image blackScreen;
    [SerializeField] GameObject player;
    [SerializeField] AudioClip startAudio;
    [SerializeField] AudioClip gaspAudio;

    AudioSource audioSource;
    RigidbodyFirstPersonController rbc;
    AudioSource playerAudio;
    GameObject weapons;
    LoadGame loadGame;
    SaveGame saveGame;

    void Start()
    {
        blackScreen.transform.gameObject.SetActive(true);
        saveGame = FindObjectOfType<SaveGame>();
        loadGame = FindObjectOfType<LoadGame>();
        audioSource = GetComponent<AudioSource>();
        rbc = player.GetComponent<RigidbodyFirstPersonController>();
        playerAudio = player.GetComponent<AudioSource>();
        weapons = player.transform.GetChild(0).GetChild(0).gameObject;
        rbc.enabled = false;
        playerAudio.enabled = false;
        Vector3 playerPosition = new Vector3(610.070f, 4.769f, 126.936f);
        saveGame.SetPlayerPosition(playerPosition);
        saveGame.enabled = true;
        weapons.SetActive(false);
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        audioSource.PlayOneShot(startAudio, 1f);
        yield return new WaitForSeconds(14);
            while(audioSource.isPlaying)
            {
                yield return null;
            }
            audioSource.PlayOneShot(gaspAudio, 1f);
            rbc.enabled = true;
            playerAudio.enabled = true;
            weapons.SetActive(true);
            loadGame.enabled = true;
            GetComponent<TriggeredScript>().isTriggered = true;
            player.transform.position = new Vector3(610.0703f, 4.7694f, 126.9368f);
            blackScreen.CrossFadeAlpha(0f, 0f, false);
    }
}
