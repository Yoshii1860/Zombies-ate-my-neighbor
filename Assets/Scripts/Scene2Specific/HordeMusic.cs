using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeMusic : MonoBehaviour
{
    [SerializeField] AudioSource trigger;
    [SerializeField] AudioSource environment;
    List<GameObject> horde = new List<GameObject>();
    int deathCount = 0;
    int num = 0;

    void Start() 
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            horde.Add(transform.GetChild(i).gameObject);
        }
        StartCoroutine(HordeDeath());
    }

    IEnumerator HordeDeath()
    {
        foreach(GameObject monster in horde)
        {
            yield return new WaitWhile(() => monster.GetComponent<EnemyHealth>().IsDead() == false);
            deathCount++;
            Debug.Log(deathCount);
            if(deathCount == transform.childCount)
            {
                Debug.Log("AllDead");
                StartCoroutine(StartFadeOut());
                StartCoroutine(StartFadeIn());
            }
        }
    }

    IEnumerator StartFadeOut()
    {
        float currentTime = 0;
        float start = trigger.volume;
        float duration = 4;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            trigger.volume = Mathf.Lerp(start, 0, currentTime / duration);
            yield return null;
        }
    }

    IEnumerator StartFadeIn()
    {
        yield return new WaitForSeconds(4);
        float currentTime = 0;
        float start = environment.volume;
        float duration = 3;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            environment.volume = Mathf.Lerp(start, 1, currentTime / duration);
            yield return null;
        }
    }
}
