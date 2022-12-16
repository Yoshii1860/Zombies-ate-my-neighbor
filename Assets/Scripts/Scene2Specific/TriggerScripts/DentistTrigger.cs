using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DentistTrigger : MonoBehaviour
{
    [SerializeField] GameObject dollStairs;
    [SerializeField] GameObject dollClassroom;
    [SerializeField] DentistDollTrigger dollTrigger;
    [SerializeField] AudioSource audioSource;

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(dollStairs.activeSelf)
            {
                dollStairs.SetActive(false);
            }
            else if(dollTrigger.triggered)
            {
                StartCoroutine(StartFade());
            }
            else return;
        }
    }

    IEnumerator StartFade()
    {
        float currentTime = 0;
        float start = audioSource.volume;
        float duration = 2;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, 0, currentTime / duration);
            yield return null;
        }
        dollTrigger.gameObject.SetActive(false);
        dollClassroom.SetActive(true);
        transform.parent.GetComponent<TriggeredScript>().isTriggered = true;
        yield break;
    }
}
