using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundKeyTrigger : MonoBehaviour
{
    [SerializeField] GameObject hordeTrigger;
    AudioSource audioSource;

    void Start() 
    {
        this.transform.parent.parent.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(KeyPickup());
        }
    }

    IEnumerator KeyPickup()
    {
        this.transform.parent.parent.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        hordeTrigger.SetActive(true);
        audioSource.Play();
        yield return new WaitWhile(() => audioSource.isPlaying);
        this.gameObject.SetActive(false);
    }
}
