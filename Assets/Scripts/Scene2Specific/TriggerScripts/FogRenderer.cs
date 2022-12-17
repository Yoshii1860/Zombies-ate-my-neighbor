using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogRenderer : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float minDistance = 10f;
    [SerializeField] float fogEndNew = 10f;
    [SerializeField] Color fogColorNew = new Color(0,0,0,1);
    Color fogColorCurrent;
    float fogEnd;
    float distance;
    public bool isInside = false;

    void Start() 
    {
        fogColorCurrent = RenderSettings.fogColor;
        fogEnd = RenderSettings.fogEndDistance;
        if(isInside)
        {
            RenderSettings.fogColor = fogColorNew;
            RenderSettings.fogEndDistance = fogEndNew;
        }
    }

    void Update() 
    {
        distance = Vector3.Distance(player.position, transform.position) / 10f;
        RenderFog();
    }

    void OnTriggerEnter(Collider other) 
    {
        if(!isInside)
        { isInside = true; } else { isInside = false; }
    }

    void RenderFog()
    {
        if(!isInside && distance <= minDistance)
        {
            StartCoroutine(FadeInFog());
        }
    }

    IEnumerator FadeInFog()
    {
        while (!isInside && distance < minDistance)
        {   
            RenderSettings.fogColor = Color.Lerp(fogColorNew, fogColorCurrent, distance);
            RenderSettings.fogEndDistance = Mathf.Lerp(fogEndNew, fogEnd, distance);
            yield return null;
        }
        yield break;
    }
}